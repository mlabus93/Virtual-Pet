using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class MoveToAction : MonoBehaviour
    {
        public GameObject eatPosition;
        public GameObject drinkPosition;
        public GameObject[] randomPositions;
        public bool inTarget = false;
        //public bool isEating = false;
        public bool isDrinking = false;
        public bool moveRandom = true;

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

        bool randomTargetFound = true;
        bool playerStopped;



        // Use this for initialization
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            anim = player.GetComponent<Animator>();
            table = GameObject.FindGameObjectWithTag("Food Table").transform;
            bed = GameObject.FindGameObjectWithTag("Bed").transform;
            toyDoll = GameObject.FindGameObjectsWithTag("Toy Doll");
            //toyBall = GameObject.FindGameObjectWithTag("Toy Ball").transform;
            toilet = GameObject.FindGameObjectWithTag("Toilet").transform;
            nav = GetComponent<NavMeshAgent>();
        }

        void FixedUpdate()
        {

            if (playerStopped)
            {
                anim.SetFloat("Speed", 0);
            }
            else
            {
                anim.SetFloat("Speed", (nav.velocity.magnitude / (Time.deltaTime * 1000)));
            }
        }


        //public void PurchaseToy(GameObject selection)
        //{
        //    int balance = GameManager._coins;

        //    if (isAble(balance, selection.GetComponent<IToy>().cost))
        //    {
        //        gameManager.AddCoins(-selection.GetComponent<IFood>().cost);
        //        UpdateAvailablity(selection.name);
        //    }
        //    else
        //    {
        //        //PRINT ERROR - play mini games to earn coin to use on foods and toys
        //        insuffientCoins.color = Color.red;
        //        StartCoroutine(RemoveErrorMessage());
        //    }
        //}

        // Update is called once per frame
        void Update()
        {

            if (!moveRandom)
            {
                if (currentTarget.Equals("table") && !inTarget)
                {
                    nav.SetDestination(table.position);
                }
                //else if(currentTarget.Equals("bed") && !inTarget)
                //{
                //    nav.SetDestination(bed.position);
                //}
                else if (currentTarget.Equals("doll"))
                {
                    if (currentDoll >= toyDoll.Length)
                    {
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
                                AnimalGameManager._player.PlayWithAnimal((toyDoll[currentDoll].GetComponent("ToySatisfaction") as ToySatisfaction));
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
                //else if (currentTarget.Equals("toilet") && !inTarget)
                //{
                //    nav.SetDestination(toilet.position);
                //}
                else if (currentTarget.Equals("bowl") && !inTarget)
                {
                    nav.SetDestination(drinkPosition.transform.position);
                }
            }
            else
            {
                MoveRandomly();
            }
        }

        public void MoveRandomly()
        {
            if (randomTargetFound)
            {
                var oldTarget = currentRandomTarget;
                int newSpot = Random.Range(0, randomPositions.Length);
                currentRandomTarget = randomPositions[newSpot].transform;
                currentRandomTarget = oldTarget == null || oldTarget != currentRandomTarget ? currentRandomTarget : randomPositions[(newSpot + 1) % randomPositions.Length].transform;
                randomTargetFound = false;
            }
            nav.SetDestination(currentRandomTarget.position);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.transform == currentRandomTarget)
            {
                randomTargetFound = true;
            }
            else if (other.tag.Equals("Toy Doll"))
            {
                inTarget = true;
            }
        }

        public void StopPlayer()
        {
            nav.Stop();
            playerStopped = true;
        }

        public void GoToWaterBowl()
        {
            currentTarget = "bowl";
        }

        public void GoToFoodPlate()
        {
            nav.SetDestination(eatPosition.transform.position);
        }

        public void GoToFoodTable()
        {
            moveRandom = false;
            inTarget = false;
            currentTarget = "table";
        }

        public void GoToBed()
        {
            inTarget = false;
            currentTarget = "bed";
        }

        public void PlayWithDoll()
        {
            moveRandom = false;

            foreach (GameObject doll in toyDoll)
            {
                doll.GetComponent<ToyDollMovement>().PlayWithDoll();
            }
            inTarget = false;
            currentTarget = "doll";
        }

        public void PlayWithBall()
        {
            inTarget = false;
            currentTarget = "ball";
        }

        public void UseRestRoom()
        {
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
    }
}

