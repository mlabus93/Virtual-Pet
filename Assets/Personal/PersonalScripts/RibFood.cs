using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class RibFood : MonoBehaviour, IFood
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

        public RibFood()
        {
            meaty = 70;
            watery = 20;
            rehydration = -5;
            calories = 50;
            cost = 30;
        }
    }
}
