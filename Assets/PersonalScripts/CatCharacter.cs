using UnityEngine;
using System.Collections;

public class CatCharacter : Character
{
    CatCharacter()
    {
       _nickName = "Mr.Kitty";

        _hungerDepletionRate = 5;
        _thirstDepletionRate = 5;
        _happinessDepletionRate = 5;
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
        _weaponHandler = GetComponent<WeaponHandler>();
        // sets up cat's preferences
        _Prefs = new AnimalPreferences(70, 10, 50, 75, 40, 15, 3);
        _petAgeTimer = gameObject.AddComponent<Timer>();
        // begin aging process
        _petAgeTimer.SetTimer(TimeLapseRate);
        _petAgeTimer.PauseUnPause();
    }

    public override PlayableCharacters GetAnimalType()
    {
        return PlayableCharacters.Cat;
    }

}
