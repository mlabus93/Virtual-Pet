// Project: Cyber Animal
// File: AnimalPreferences.cs
// Modification History:
// Author           Date
// Jean-Baptiste    11/22/15

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public class AnimalPreferences
    {
        private const int FULL = 100;
        private const int EMPTY = 0;

        int _fishPref;
        int _meatPref;
        int _waterPref;
        int _sweetPref;

        int _bouncePref;
        int _squishPref;
        int _smoothPref;

        //public int hunger { get { return (_hunger); } set { _hunger = ClampAtFull(value); } }

        // food preferences
        public int fishPref { get { return (_fishPref); } set { _fishPref = ClampAtFull(value); } }
        public int meatPref { get { return (_meatPref); } set { _meatPref = ClampAtFull(value); } }
        public int wateryPref { get { return (_waterPref); } set { _waterPref = ClampAtFull(value); } }
        public int sweetPref { get { return (_sweetPref); } set { _sweetPref = ClampAtFull(value); } }

        // toy preferences
        public int bouncePref { get { return (_bouncePref); } set { _bouncePref = ClampAtFull(value); } }
        public int squishPref { get { return (_squishPref); } set { _squishPref = ClampAtFull(value); } }
        public int smoothPref { get { return (_smoothPref); } set { _smoothPref = ClampAtFull(value); } }

        public AnimalPreferences(int fsh, int mt, int wter, int swt, int bncy, int sqsh, int smth)
        {
            fishPref = fsh;
            meatPref = mt;
            wateryPref = wter;
            sweetPref = swt;
            bouncePref = bncy;
            squishPref = sqsh;
            smoothPref = smth;
        }

        public int ClampAtFull(int val)
        {
            if (val > FULL)
                return FULL;
            if (val < EMPTY)
                return EMPTY;
            return val;
        }


    }

