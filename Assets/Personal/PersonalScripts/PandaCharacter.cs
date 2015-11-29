// Project: Pet Pals
// File: PandaCharacter.cs
// Modification History:
// Author           Date
// Jean-Baptiste    11/22/15
// Jean-Baptiste    11/28/15

using UnityEngine;
using System.Collections;


namespace PersonalScripts
{
    public class PandaCharacter : Character
    {

        PandaCharacter()
        {
            _nickName = "Mr.Panda";

            // animal depletion rates
            _hungerDepletionRate = 4;
            _thirstDepletionRate = 2;
            _happinessDepletionRate = 1;
            _fatigueDepeletionRate = 1;
            _bladderCapacityDepletionRate = 3;
            _boredomDepletionRate = 6;
            _TimeLapseRate = 5; // rate at which animal traits deplete/ refresh in sec
        }

        public override void Start()
        {
            ResetStatuses();
            SetandReturnOutfitSystem();
            _anim = GetComponent<Animator>();
            // sets up panda's preferences
            _Prefs = new AnimalPreferences(70, 10, 50, 75, 40, 15, 3);
            _petAgeTimer = gameObject.AddComponent<Timer>();
            // begin aging process
            _petAgeTimer.SetTimer(TimeLapseRate);
            _petAgeTimer.PauseUnPause();
            _weaponHandler = GetComponent<WeaponHandler>();
        }

        public override PlayableCharacters GetAnimalType()
        {
            return PlayableCharacters.Panda;
        }

        public override OutfitChange SetandReturnOutfitSystem()
        {
            //_outfitSystem = FindObjectOfType<OutfitChange>();
            _outfitSystem = transform.Find("animal_ch_panda_mesh").gameObject.GetComponent<OutfitChange>();

            if (_outfitSystem == null)
                Debug.LogError("THE OUTFIT WAS NOT FOUND");

            return _outfitSystem;
        }

    }

}
