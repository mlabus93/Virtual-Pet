using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PersonalScripts
{
    public class MoveToAction : MonoBehaviour
    {
        public bool inTarget = false;
        public bool moveRandom = true;
        public bool randomTargetFound = true;

        GameObject[] randomPositions;
        AnimalGameManager gameManager;
        GameObject insuffientCoins;
        Animator anim;
        GameObject player;
        Transform table;
        Transform bed;
        GameObject[] toyDoll;
        Transform toyBall;
        Transform toilet;
        Transform currentRandomTarget;
        NavMeshAgent nav;
        string currentTarget = "";
        int currentDoll = 0;
        bool isPlaying;
        bool playerStopped;
        bool readToSleep = false;
        Character playerScript;

        // Use this for initialization
        void Start()
        {
            gameManager = FindObjectOfType<AnimalGameManager>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent("Character") as Character;
            anim = player.GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
        }

        void FixedUpdate()
        {
            if (Application.loadedLevelName.Equals("Main"))
            {
                if (playerStopped)
                {
                    anim.SetFloat("Speed", 0);
                }
                else
                {
                    anim.SetFloat("Speed", (nav.velocity.magnitude));/// (Time.deltaTime * 100)));
                }
            }
        }

        public bool isAbleToBuy(int balance, int cost)
        {
            return (balance - cost) >= 0 ? true : false;
        }

        public void PurchaseToy(int costToPlay)
        {
            int balance = gameManager.GetCoins(); 

            if (isAbleToBuy(balance, costToPlay))
            {
                gameManager.AddCoins(-costToPlay);
                isPlaying = true;
            }
            else
            {
                //PRINT ERROR - play mini games to earn coin to use on foods and toys
                insuffientCoins = (GameObject.FindWithTag("UIManager").GetComponent("UIController") as UIController).insufficientCoinsTxt;
                insuffientCoins.SetActive(true);
                StartCoroutine(RemoveErrorMessage());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Application.loadedLevelName.Equals("Main"))
            {
                if (!moveRandom)
                {
                    if (currentTarget.Equals("table") && !inTarget)
                    {
                        nav.SetDestination(table.position);
                    }
                    else if (currentTarget.Equals("doll"))
                    {
                        if (currentDoll >= toyDoll.Length)
                        {
                            currentDoll = 0;
                            currentTarget = "";
                            moveRandom = true;
                            inTarget = false;
                            foreach (GameObject doll in toyDoll)
                            {
                                doll.GetComponent<DollHealth>().Reset();
                            }
                        }
                        else
                        {
                            if (inTarget)
                            {
                                DollHealth currentDollHealth = toyDoll[currentDoll].GetComponent("DollHealth") as DollHealth;
                                if (currentDollHealth.currentHealth > 0)
                                {
                                    playerScript.PlayWithAnimal((toyDoll[currentDoll].GetComponent("ToySatisfaction") as ToySatisfaction));
                                    Attack(Random.Range(1, 2));
                                    currentDollHealth.TakeDamage(10, player.transform.position);
                                }
                                else
                                {
                                    currentDoll++;
                                    inTarget = false;
                                }
                            }
                            else
                            {
                                nav.SetDestination(toyDoll[currentDoll].transform.position);
                            }
                        }
                    }
                    //else if (currentTarget.Equals("ball") && !inTarget)
                    //{
                    //    nav.SetDestination(toyBall.position);
                    //}
                    else if (currentTarget.Equals("bed"))
                    {
                        if (inTarget)
                        {
                            StopPlayer();
                            float sleepLength = Random.Range(60f, 180f);
                            playerScript.Sleep(sleepLength);
                            playerScript.Rested();
                            StartCoroutine(ReturnFromClickState(sleepLength));
                            inTarget = false;
                        }
                        else
                        {
                            nav.SetDestination(bed.position);
                        }

                    }
                    else if (currentTarget.Equals("toilet"))
                    {
                        if (inTarget)
                        {
                            StopPlayer();
                            float bathroomLength = Random.Range(10f, 45f);
                            switch (Random.Range(1, 4))
                            {
                                case 1:
                                    playerScript.Putup(bathroomLength);
                                    break;
                                case 2:
                                    playerScript.SayGoodbye(bathroomLength);
                                    break;
                                case 3:
                                    playerScript.Talk(bathroomLength);
                                    break;
                                case 4:
                                    playerScript.TakeUp(bathroomLength);
                                    break;
                            }
                            playerScript.EmptyBladder();
                            StartCoroutine(ReturnFromClickState(bathroomLength));
                            inTarget = false;
                        }
                        else
                        {
                            nav.SetDestination(toilet.position);
                        }
                    }
                }
                else
                {
                    MoveRandomly();
                }
            }
        }

        public void MoveRandomly()
        {
            randomPositions = GameObject.FindGameObjectsWithTag("RandomPosition");
            if (randomTargetFound)
            {
                var oldTarget = currentRandomTarget;
                int newSpot = Random.Range(0, randomPositions.Length - 1);
                currentRandomTarget = randomPositions[newSpot].transform;
                currentRandomTarget = oldTarget == null || oldTarget != currentRandomTarget ? currentRandomTarget : randomPositions[(newSpot + 1) % randomPositions.Length].transform;
                randomTargetFound = false;
            }
            nav.SetDestination(currentRandomTarget.position);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform == currentRandomTarget && moveRandom)
            {
                randomTargetFound = true;
            }
            else if (other.tag.Equals("Toy Doll") && currentTarget.Equals("doll") && currentDoll <= toyDoll.Length && other.transform == toyDoll[currentDoll].transform)
            {
                inTarget = true;
            }
            else if (other.tag.Equals("Bed") && currentTarget.Equals("bed"))
            {
                inTarget = true;
            }
            else if (other.tag.Equals("Toilet") && currentTarget.Equals("toilet"))
            {
                inTarget = true;
            }
        }

        public void StopPlayer()
        {
            nav.Stop();
            playerStopped = true;
        }

        public void GoToFoodTable()
        {
            table = GameObject.FindGameObjectWithTag("Food Table").transform;
            moveRandom = false;
            inTarget = false;
            currentTarget = "table";
            StartCoroutine(ReturnToRadom());
        }
     
        public void GoToBed()
        {
            bed = GameObject.FindGameObjectWithTag("Bed").transform;
            moveRandom = false;
            inTarget = false;
            currentTarget = "bed";
        }

        public void PlayWithDoll()
        {
            toyDoll = GameObject.FindGameObjectsWithTag("Toy Doll");
            int costToPlay = 0;
            foreach (GameObject doll in toyDoll)
            {
                costToPlay += (doll.GetComponent("ToySatisfaction") as ToySatisfaction).cost;
            }

            PurchaseToy(costToPlay);
            if (isPlaying)
            {
                moveRandom = false;

                foreach (GameObject doll in toyDoll)
                {
                    doll.GetComponent<ToyDollMovement>().PlayWithDoll();
                }
                inTarget = false;
                currentTarget = "doll";
                isPlaying = false;
            }
        }

        public void PlayWithBall()
        {
            //toyBall = GameObject.FindGameObjectWithTag("Toy Ball").transform;
            inTarget = false;
            currentTarget = "ball";
        }

        public void UseRestRoom()
        {
            toilet = GameObject.FindGameObjectWithTag("Toilet").transform;
            moveRandom = false;
            inTarget = false;
            currentTarget = "toilet";
        }

        void Attack(int attackType)
        {
            if (attackType == 1)
            {
                anim.SetTrigger("Attack1");
            }
            if (attackType == 2)
            {
                anim.SetTrigger("Attack2");
            }
        }

        IEnumerator RemoveErrorMessage()
        {
            yield return new WaitForSeconds(4f);
            insuffientCoins.SetActive(false);
        }

        IEnumerator ReturnToRadom ()
        {
            yield return new WaitForSeconds(30f);
            currentTarget = "";
            randomTargetFound = true;
            moveRandom = true;
            nav.Resume();
            playerStopped = false;
        }

        IEnumerator ReturnFromClickState(float amount)
        {
            yield return new WaitForSeconds(amount - 20f);
            StartCoroutine(ReturnToRadom());
        }
    }
}
