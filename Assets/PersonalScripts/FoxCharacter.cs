using UnityEngine;
using System.Collections;

public class FoxCharacter : Character
{

    FoxCharacter()
    {
        _nickName = "Mr.Fox";

        _hungerDepletionRate = 12;
        _thirstDepletionRate = 5;
        _happinessDepletionRate = 2;
        _fatigueDepeletionRate = 1;
        _bladderCapacityDepletionRate = 1;
        _boredomDepletionRate = 6;
        _TimeLapseRate = 5; // rate at which animal traits deplete/ refresh in sec
    }

    public override void Start()
    {
        ResetStatuses();
        SetandReturnOutfitSystem();
        _anim = GetComponent<Animator>();
        // sets up fox's preferences
        _Prefs = new AnimalPreferences(70, 10, 50, 75, 40, 15, 3);
        _petAgeTimer = gameObject.AddComponent<Timer>();
        // begin aging process
        _petAgeTimer.SetTimer(TimeLapseRate);
        _petAgeTimer.PauseUnPause();
        _weaponHandler = GetComponent<WeaponHandler>();
    }

    public override PlayableCharacters GetAnimalType()
    {
        return PlayableCharacters.Fox;
    }
 
}
