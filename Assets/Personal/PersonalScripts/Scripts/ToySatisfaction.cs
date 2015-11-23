using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class ToySatisfaction : MonoBehaviour, IToy
    {
        public int bounceValue;
        public int squishValue;
        public int smoothValue;
        public int funValue;
        public int costValue;

        // desireable toy traits
        public int bouncy { get; set; }
        public int squishey { get; set; }
        public int smooth { get; set; }
        // Toy value
        public int funLevel { get; set; }
        // cost of toy (in coins)
        public int cost { get; set; }

        public ToySatisfaction()
        {

        }

        void Awake()
        {
            bouncy = bounceValue;
            squishey = squishValue;
            smooth = smoothValue;
            funLevel = funValue;
            cost = costValue;
        }
    }
}

