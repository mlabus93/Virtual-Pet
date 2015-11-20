using UnityEngine;
using System.Collections;

public class ManageFoods : MonoBehaviour {
    public GameManager gameManager;
    public GameObject turkey;
    public GameObject ribs;
    public GameObject chicken;
    public GameObject fish;
    public GameObject beef;
    public GameObject treat;
    public GameObject water;
    
    private int turkeyCount;
    private int ribCount;
    private int chickenCount;
    private int fishCount;
    private int beefCount;
    private int treatCount;
    private bool isEating;

    public void PurchaseFood(GameObject selection)
    {
        int balance = GameManager._coins;

        if(isAble(balance, selection.GetComponent<IFood>().cost))
        {
            gameManager.AddCoins(-selection.GetComponent<IFood>().cost);
            UpdateAvailablity(selection.name);
        }
        else
        {
            //PRINT ERROR - play mini games to earn coin to useon foods and toys
        }
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
        GameManager._player.FeedAnimal(water.GetComponent<WaterDrink>());
        isEating = false;
    }

    public void EatTurkey()
    {
        if (!isEating)
        {
            PurchaseFood(turkey);

            if (isAble(turkeyCount, 1))
            {
                isEating = true;
                turkey.SetActive(true);
                GameManager._player.FeedAnimal(turkey.GetComponent<TurkeyFood>());
                turkey.SetActive(false);
                turkeyCount--;
                isEating = false;
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
                chicken.SetActive(true);
                GameManager._player.FeedAnimal(chicken.GetComponent<ChickenFood>());
                chicken.SetActive(false);
                chickenCount--;
                isEating = false;
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
                ribs.SetActive(true);
                GameManager._player.FeedAnimal(ribs.GetComponent<RibFood>());
                ribs.SetActive(false);
                ribCount--;
                isEating = false;
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
                beef.SetActive(true);
                GameManager._player.FeedAnimal(beef.GetComponent<BeefFood>());
                beef.SetActive(false);
                beefCount--;
                isEating = false;
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
                fish.SetActive(true);
                GameManager._player.FeedAnimal(fish.GetComponent<FishFood>());
                fish.SetActive(false);
                fishCount--;
                isEating = false;
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
                treat.SetActive(true);
                GameManager._player.FeedAnimal(treat.GetComponent<TreatFood>());
                treat.SetActive(false);
                treatCount--;
                isEating = false;
            }
        }
    }

}


