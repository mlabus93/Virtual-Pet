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

    public void PurchaseFood(GameObject selection)
    {
        int balance = GameManager._coins;

        if(isAble(balance, selection.GetComponent<IFood>().cost))
        {
            gameManager.AddCoins(-selection.GetComponent<IFood>().cost);
            UpdateAvailablity(selection.name);
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
        //DrinkWater drink = new DrinkWater();
        //GameManager._player.FeedAnimal(drink);
    }

    public void EatTurkey()
    {
        TurkeyFood james = turkey.GetComponent("TurkeyFood") as TurkeyFood;
        TurkeyFood james2 = turkey.GetComponent<TurkeyFood>();
        GameManager._player.FeedAnimal(james);
        EatFood("Turkey");
    }

    public void EatChicken()
    {
        EatFood("Chicken");
    }

    public void EatRibs()
    {
        EatFood("Ribs");
    }

    public void EatBeef()
    {
        EatFood("Beef");
    }

    public void EatFish()
    {
        EatFood("Fish");
    }

    public void EatTreat()
    {
        EatFood("Treat");
    }

    private void EatFood(string selection)
    {
        //wait until eating animation is done to remove item
        //while (GetComponent<Animation>().IsPlaying("A_eat")) ;

        switch (selection)
        {
            case "Turkey":
                if(isAble(turkeyCount, 1))
                {
                    turkey.SetActive(false);
                    turkeyCount--;
                }
                break;
            case "Chicken":
                if (isAble(chickenCount, 1))
                {
                    chicken.SetActive(false);
                    chickenCount--;
                }
                break;
            case "Ribs":
                if (isAble(ribCount, 1))
                {
                    ribs.SetActive(false);
                    ribCount--;
                }
                break;
            case "Beef":
                if (isAble(beefCount, 1))
                {
                    beef.SetActive(false);
                    beefCount--;
                }
                    break;
            case "Fish":
                if (isAble(fishCount, 1))
                {
                    fish.SetActive(false);
                    fishCount--;
                }
                    break;
            case "Treat":
                if (isAble(treatCount, 1))
                {
                    treat.SetActive(false);
                    treatCount--;
                }
                break;
            default:
                break;
        }  
    }

    public void SetPlate(string item)
    {
        switch (item)
        {
            case "Turkey":
                turkey.SetActive(true);
                break;
            case "Chicken":
                chicken.SetActive(true);
                break;
            case "Ribs":
                ribs.SetActive(true);
                break;
            case "Beef":
                beef.SetActive(true);
                break;
            case "Fish":
                fish.SetActive(true);
                break;
            case "Treat":
                treat.SetActive(true);
                break;
            default:
                break;
        }
    }
}


