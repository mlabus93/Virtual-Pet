using UnityEngine;
using System.Collections;

public class DogCharacter : Character
{
    DogCharacter()
    {
        _nickName = "Mr.Doggy";

        // animal depletion rates
        _hungerDepletionRate = 10;
        _thirstDepletionRate = 5;
        _happinessDepletionRate = 8;
        _fatigueDepeletionRate = 5;
        _bladderCapacityDepletionRate = 10;
        _boredomDepletionRate = 4;
        _TimeLapseRate = 5; // rate at which animal traits deplete/ refresh in sec
    }

    public override void Start()
    {
        ResetStatuses();
        SetandReturnOutfitSystem();
        _anim = GetComponent<Animator>();
        // sets up dog's preferences
        _Prefs = new AnimalPreferences(70, 10, 50, 75, 40, 15, 3);
        _petAgeTimer = gameObject.AddComponent<Timer>();
        // begin aging process
        _petAgeTimer.SetTimer(TimeLapseRate);
        _petAgeTimer.PauseUnPause();
        _weaponHandler = GetComponent<WeaponHandler>();
    }

    public override PlayableCharacters GetAnimalType()
    {
        return PlayableCharacters.Dog;
    }
}
