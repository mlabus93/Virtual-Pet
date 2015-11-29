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
        //Transform toyBall;
        Transform toilet;
        Transform currentRandomTarget;
        NavMeshAgent nav;
        string currentTarget = "";
        int currentDoll = 0;
        bool isPlaying;
        bool playerStopped;
        Character playerScript;
        bool isDefaulSet;
        bool timesUp;
        bool findNext = true;

        void SetupMoveAction()
        {
            gameManager = FindObjectOfType<AnimalGameManager>();
            player = GameObject.FindGameObjectWithTag("Player");
            playerScript = player.GetComponent("Character") as Character;
            anim = player.GetComponent<Animator>();
            nav = GetComponent<NavMeshAgent>();
            nav.enabled = true;
        }

        void FixedUpdate()
        {
                if(!isDefaulSet)
                {
                    isDefaulSet = true;
                    SetupMoveAction();
                }

                if (playerStopped)
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Eat"))
                    {
                        anim.SetFloat("Speed", 0);
                    }
                }
                else
                {
                    anim.SetFloat("Speed", (nav.velocity.magnitude));/// (Time.deltaTime * 100)));
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
                insuffientCoins = (GameObject.Find("CanvasMain(Clone)").GetComponent("UIController") as UIController).insufficientCoinsTxt;
                insuffientCoins.SetActive(true);
                StartCoroutine(RemoveErrorMessage());
            }
        }

        // Update is called once per frame
        void Update()
        {
                if (!moveRandom)
                {
                    if (currentTarget.Equals("table") && !inTarget)
                    {
                        nav.SetDestination(table.position);
                    }
                    else if (currentTarget.Equals("doll"))
                    {
                        if(timesUp)
                        {
                            
                            for( ; currentDoll < toyDoll.Length; currentDoll++)
                            {
                                (toyDoll[currentDoll].GetComponent("DollHealth") as DollHealth).TakeDamage(100, player.transform.position);
                            }
                        }
                        if (currentDoll >= toyDoll.Length)
                        {
                            isPlaying = false;
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
                                if(findNext)
                                {
                                    nav.SetDestination(toyDoll[currentDoll].transform.position);
                                    findNext = false;
                                    StartCoroutine(FindNextDoll());
                                }
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
                            float sleepLength = Random.Range(30f, 60f);
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
                            float bathroomLength = Random.Range(5f, 15f);
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
        void OnTriggerStay(Collider other)
        {
            if(isPlaying && other.tag.Equals("Toy Doll") && currentTarget.Equals("doll") && currentDoll <= toyDoll.Length && other.transform == toyDoll[currentDoll].transform)
            {
                inTarget = true;
            }
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
            StartCoroutine(ReturnToRadom(30f));
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
            if (!isPlaying)
            {
                timesUp = false;
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
                    //isPlaying = false;
                    StartCoroutine (StopPlaying());
                 }
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

        IEnumerator ReturnToRadom (float amount)
        {
            yield return new WaitForSeconds(amount);
            currentTarget = "";
            randomTargetFound = true;
            moveRandom = true;
            nav.Resume();
            playerStopped = false;
        }

        IEnumerator ReturnFromClickState(float amount)
        {
            yield return new WaitForSeconds(amount);
            StartCoroutine(ReturnToRadom(5));
        }

        IEnumerator StopPlaying()
        {
            yield return new WaitForSeconds(120f);
            if (isPlaying)
            {
                timesUp = true;
            }
        }
        IEnumerator FindNextDoll()
        {
            yield return new WaitForSeconds(3f);
            findNext = true;
        }
    }
}
