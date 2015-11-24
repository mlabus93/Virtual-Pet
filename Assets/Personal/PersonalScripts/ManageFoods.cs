using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PersonalScripts
{
    public class ManageFoods : MonoBehaviour
    {
        public AnimalGameManager gameManager;
        public GameObject turkey;
        public GameObject ribs;
        public GameObject chicken;
        public GameObject fish;
        public GameObject beef;
        public GameObject treat;
        public GameObject water;
        public GameObject insuffientCoins;

        private int turkeyCount;
        private int ribCount;
        private int chickenCount;
        private int fishCount;
        private int beefCount;
        private int treatCount;
        private bool isEating;
        Character player;

        public void PurchaseFood(GameObject selection)
        {
            int balance = gameManager.GetCoins();

            if (isAble(balance, selection.GetComponent<IFood>().cost))
            {
                gameManager.AddCoins(-selection.GetComponent<IFood>().cost);
                UpdateAvailablity(selection.name);
            }
            else
            {
                //PRINT ERROR - play mini games to earn coin to use on foods and toys
                insuffientCoins.SetActive(true);
                //insuffientCoins.enabled = true;
                //insuffientCoins.color = Color.red;
                StartCoroutine(RemoveErrorMessage());
            }
        }

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent("Character") as Character;

        }

        private void UpdateAvailablity(string item)
        {
            switch (item)
            {
                case "Turkey":
                    turkeyCount++;
                    break;
                case "Chicken":
                    chickenCount++;
                    break;
                case "Ribs":
                    ribCount++;
                    break;
                case "Beef":
                    beefCount++;
                    break;
                case "Fish":
                    fishCount++;
                    break;
                case "Treat":
                    treatCount++;
                    break;
                default:
                    break;
            }
        }


        public bool isAble(int balance, int selection)
        {
            return (balance - selection) >= 0 ? true : false;
        }

        public void Drink()
        {
            isEating = true;
            MeshRenderer skin = turkey.GetComponent("MeshRenderer") as MeshRenderer;
            player.FeedAnimal((water.GetComponent("WaterDrink") as WaterDrink));
            StartCoroutine(DelayForFeeding(water));
        }

        public void EatTurkey()
        {
            if (!isEating)
            {
                PurchaseFood(turkey);

                if (isAble(turkeyCount, 1))
                {
                    isEating = true;
                    MeshRenderer skin = turkey.GetComponent("MeshRenderer") as MeshRenderer;
                    skin.enabled = true;
                    player.FeedAnimal((turkey.GetComponent("TurkeyFood") as TurkeyFood));
                    StartCoroutine(DelayForFeeding(turkey));
                    turkeyCount--;
                }
            }
        }

        public void EatChicken()
        {
            if (!isEating)
            {
                PurchaseFood(chicken);

                if (isAble(chickenCount, 1))
                {
                    isEating = true;
                    MeshRenderer skin = chicken.GetComponent("MeshRenderer") as MeshRenderer;
                    skin.enabled = true;
                    player.FeedAnimal((chicken.GetComponent("ChickenFood") as ChickenFood));
                    StartCoroutine(DelayForFeeding(chicken));
                    chickenCount--;
                }
            }
        }

        public void EatRibs()
        {
            if (!isEating)
            {
                PurchaseFood(ribs);

                if (isAble(ribCount, 1))
                {
                    isEating = true;
                    MeshRenderer skin = ribs.GetComponent("MeshRenderer") as MeshRenderer;
                    skin.enabled = true;
                    player.FeedAnimal((ribs.GetComponent("RibFood") as RibFood));
                    StartCoroutine(DelayForFeeding(ribs));
                    ribCount--;
                }
            }
        }

        public void EatBeef()
        {
            if (!isEating)
            {
                PurchaseFood(beef);

                if (isAble(beefCount, 1))
                {
                    isEating = true;
                    MeshRenderer skin = beef.GetComponent("MeshRenderer") as MeshRenderer;
                    skin.enabled = true;
                    player.FeedAnimal((beef.GetComponent("BeefFood") as BeefFood));
                    StartCoroutine(DelayForFeeding(beef));
                    beefCount--;
                }
            }
        }

        public void EatFish()
        {
            if (!isEating)
            {
                PurchaseFood(fish);

                if (isAble(fishCount, 1))
                {
                    isEating = true;
                    SkinnedMeshRenderer skin = fish.GetComponentInChildren(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
                    skin.enabled = true;
                    player.FeedAnimal((fish.GetComponent("FishFood") as FishFood));
                    StartCoroutine(DelayForFeeding(fish));
                    fishCount--;
                }
            }
        }

        public void EatTreat()
        {
            if (!isEating)
            {
                PurchaseFood(treat);

                if (isAble(treatCount, 1))
                {
                    isEating = true; 
                    MeshRenderer skin = treat.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;
                    skin.enabled = true;
                    player.FeedAnimal((treat.GetComponent("TreatFood") as TreatFood));
                    StartCoroutine(DelayForFeeding(treat));
                    treatCount--;
                }
            }
        }

        IEnumerator DelayForFeeding(GameObject selection)
        {
            yield return new WaitForSeconds(3f);
            Renderer skin;

            if (!selection.name.Equals("BowlWater"))
            {
                if (selection.name.Equals("Fish"))
                {
                    skin = selection.GetComponentInChildren(typeof(SkinnedMeshRenderer)) as SkinnedMeshRenderer;
                }
                else if (selection.name.Equals("Treat"))
                {
                    skin = selection.GetComponentInChildren(typeof(MeshRenderer)) as MeshRenderer;
                }
                else
                {
                    skin = selection.GetComponent("MeshRenderer") as MeshRenderer;
                }
                skin.enabled = false;
            }
            isEating = false;
        }

        IEnumerator RemoveErrorMessage()
        {
            yield return new WaitForSeconds(4f);
            insuffientCoins.SetActive(false);
            //insuffientCoins.color = new Color(0, 0, 0, 0);
            //insuffientCoins.enabled = false;
        }
    }
}
