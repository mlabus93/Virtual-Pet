// Project: Pet Pals
// File: PenguinCharacter.cs
// Modification History:
// Author           Date
// Jean-Baptiste    11/22/15
// Jean-Baptiste    11/28/15

using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class PenguinCharacter : Character
    {

        PenguinCharacter()
        {
            _nickName = "Mr.Penguin";

            // animal depletion rates
            _hungerDepletionRate = 5;
            _thirstDepletionRate = 5;
            _happinessDepletionRate = 8;
            _fatigueDepeletionRate = 1;
            _bladderCapacityDepletionRate = 2;
            _boredomDepletionRate = 8;
            _TimeLapseRate = 5; // rate at which animal traits deplete/ refresh in sec
        }

        public override void Start()
        {
            ResetStatuses();
            SetandReturnOutfitSystem();
            _anim = GetComponent<Animator>();
            // sets up penguin's preferences
            _Prefs = new AnimalPreferences(70, 10, 50, 75, 40, 15, 3);
            _petAgeTimer = gameObject.AddComponent<Timer>();
            // begin aging process
            _petAgeTimer.SetTimer(TimeLapseRate);
            _petAgeTimer.PauseUnPause();
            _weaponHandler = GetComponent<WeaponHandler>();
        }

        public override PlayableCharacters GetAnimalType()
        {
            return PlayableCharacters.Penguin;
        }

        public override OutfitChange SetandReturnOutfitSystem()
        {
            //_outfitSystem = FindObjectOfType<OutfitChange>();
            _outfitSystem = transform.Find("animal_ch_peng_mesh").gameObject.GetComponent<OutfitChange>();

            if (_outfitSystem == null)
                Debug.LogError("THE OUTFIT WAS NOT FOUND");

            return _outfitSystem;
        }

    }

}
