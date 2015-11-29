using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class BeefFood : MonoBehaviour, IFood
    {
        public int fishey { get; set; }
        public int meaty { get; set; }
        public int watery { get; set; }
        public int sweet { get; set; }
        // food value
        public int rehydration { get; set; }
        public int calories { get; set; }
        // cost of food (in coins)
        public int cost { get; set; }

        public BeefFood()
        {
            meaty = 100;
            watery = 20;
            calories = 60;
            rehydration = -2;
            cost = 30;
        }
    }
}
