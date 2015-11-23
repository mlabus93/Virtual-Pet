using UnityEngine;
using System.Collections;


namespace PersonalScripts
{
    public class RabbitCharacter : Character
    {
        RabbitCharacter()
        {
            _nickName = "Mr.Rabbit";

            // animal depletion rates
            _hungerDepletionRate = 4;
            _thirstDepletionRate = 10;
            _happinessDepletionRate = 1;
            _fatigueDepeletionRate = 1;
            _bladderCapacityDepletionRate = 3;
            _boredomDepletionRate = 3;
            _TimeLapseRate = 5; // rate at which animal traits deplete/ refresh in sec
        }

        public override void Start()
        {
            ResetStatuses();
            SetandReturnOutfitSystem();
            _anim = GetComponent<Animator>();
            // sets up rabbit's preferences
            _Prefs = new AnimalPreferences(70, 10, 50, 75, 40, 15, 3);
            _petAgeTimer = gameObject.AddComponent<Timer>();
            // begin aging process
            _petAgeTimer.SetTimer(TimeLapseRate);
            _petAgeTimer.PauseUnPause();
            _weaponHandler = GetComponent<WeaponHandler>();
        }

        public override PlayableCharacters GetAnimalType()
        {
            return PlayableCharacters.Rabbit;
        }

        public override OutfitChange SetandReturnOutfitSystem()
        {
            //_outfitSystem = FindObjectOfType<OutfitChange>();
            _outfitSystem = transform.Find("animal_ch_rabbit_mesh").gameObject.GetComponent<OutfitChange>();

            if (_outfitSystem == null)
                Debug.LogError("THE OUTFIT WAS NOT FOUND");

            return _outfitSystem;
        }

    }

}
