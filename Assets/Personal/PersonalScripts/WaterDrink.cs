// Project: Pet Pals
// File: WaterDrink.cs
// Modification History:
// Author           Date
// Mirvil           11/23/15
// Mirvil           11/29/15

using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class WaterDrink : MonoBehaviour, IFood
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

        public WaterDrink()
        {
            rehydration = 100;
            cost = 10;
        }
    }
}
