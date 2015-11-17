using UnityEngine;
using System.Collections;

public interface IDamageable
{
    void TakeDamage(int amount, Vector3 hitPoint);
    void Death();
}

public interface IGivePoints
{
    void GivePoints(int amount);
}

public interface IInteractable
{
    void Interact();
}

public interface IFood
{
    // NOTE: handles food and water
    // desireable food traits
    int fishey { get; set; }
    int meaty { get; set; }
    int watery { get; set; }
    int sweet { get; set; }
    // food value
    int rehydration { get; set; }
    int calories { get; set; }
    // cost of food (in coins)
    int cost { get; set; }
}

public interface IToy
{
    // desireable toy traits
    int bouncy { get; set; }
    int squishey { get; set; }
    int smooth { get; set; }
    // Toy value
    int funLevel { get; set; }
    // cost of toy (in coins)
    int cost { get; set; }
}


public interface IAnimalCharacter
{
    // animal status traits
    [SerializeField]
    int hunger { get; set; }
    [SerializeField]
    int thirst { get; set; }
    [SerializeField]
    int happiness { get; set; }
    [SerializeField]
    int health { get; set; }
    [SerializeField]
    int fatigue { get; set; }
    [SerializeField]
    int bladderCapacity { get; set; }
    int boredom { get; set; }
    PlayableCharacters GetAnimalType();
    OutfitChange SetandReturnOutfitSystem();
    void FeedAnimal(IFood food);
    void PlayWithAnimal(IToy toy);
    void RotateThroughFits();
    void ChangeIntoSpecificFit(int fitIndex);
    Vector3 GetAnimalPosition();
    void SetAnimalPosition(Vector3 loci);
    string GetNickName();
    void CalculateHealth();
    void AgePet();
    void SayGoodbye();
    void SayGoodbye(float byeTime);
    void StopSayingGoodbye();
    void Putup();
    void Putup(float upTime);
    void PutDown();
    void Talk();
    void Talk(float convoTime);
    void StopTalking();
    void TakeUp();
    void TakeUp(float upTime);
    void TakeDown();
    void Eat(float eatTime);
    void StopEating();
    void Sleep();
    void Sleep(float sleepTime);
    void AdjustAgingRate(float ageRate);
    float GetAgeRate();
}