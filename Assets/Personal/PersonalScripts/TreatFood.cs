using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class TreatFood : MonoBehaviour, IFood
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

        public TreatFood()
        {
            sweet = 100;
            rehydration = -10;
            calories = 10;
            cost = 50;
        }
    }
}
