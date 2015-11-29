using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class TurkeyFood : MonoBehaviour, IFood
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

        public TurkeyFood()
        {
            meaty = 60;
            watery = 50;
            calories = 40;
            rehydration = -5;
            cost = 15;
        }
    }
}
