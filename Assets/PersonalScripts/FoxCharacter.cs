using UnityEngine;
using System.Collections;

public class FoxCharacter : MonoBehaviour, IAnimalCharacter
{
    private const int FULL = 100;
    private const int EMPTY = 0;
    string _nickName = "Mr.Fox";

    // animal status traits and depletion rates
    public AnimalPreferences _foxPrefs;
    int _hunger, _hungerDepletionRate = 12;
    int _thirst, _thirstDepletionRate = 5;
    int _happiness, _happinessDepletionRate = 2;
    int _fatigue, _fatigueDepeletionRate = 1;
    int _bladderCapacity, _bladderCapacityDepletionRate = 1;
    int _boredom, _boredomDepletionRate = 6;
    int _health; // NOTE: Health is calculated as an average of all of the other status fields
    // setters and getters for traits
    [SerializeField]
    public int hunger { get { return (_hunger); } set { _hunger = ClampAtFull(value); } }
    [SerializeField]
    public int thirst { get { return (_thirst); } set { _thirst = ClampAtFull(value); } }
    [SerializeField]
    public int happiness { get { return (_happiness); } set { _happiness = ClampAtFull(value); } }
    [SerializeField]
    public int health { get { return (_health); } set { _health = ClampAtFull(value); } }
    [SerializeField]
    public int fatigue { get { return (_fatigue); } set { _fatigue = ClampAtFull(value); } }
    [SerializeField]
    public int bladderCapacity { get { return (_bladderCapacity); } set { _bladderCapacity = ClampAtFull(value); } }
    [SerializeField]
    public int boredom { get { return (_boredom); } set { _boredom = ClampAtFull(value); } }

    public int ClampAtFull(int val)
    {
        if (val > FULL)
            return FULL;
        if (val < EMPTY)
            return EMPTY;
        return val;
    }

    public void AdjustAgingRate(float ageRate)
    {
        _petAgeTimer.SetTimer(_petAgeTimer._stopTime + ageRate);
    }

    public float GetAgeRate()
    {
        return _petAgeTimer._stopTime;
    }

    OutfitChange _outfitSystem;
    public Animator _anim;
    public Timer _petAgeTimer;
    public int _TimeLapseRate = 5; // rate at which animal traits deplete/ refresh in sec
    [SerializeField]
    public int TimeLapseRate { get { return _TimeLapseRate; } set { _TimeLapseRate = value; } }

    public PlayableCharacters GetAnimalType()
    {
        return PlayableCharacters.Fox;
    }
    public Vector3 GetAnimalPosition()
    {
        return GetComponent<Rigidbody>().position;
    }
    public void SetAnimalPosition(Vector3 loci)
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.position = loci;
    }
    public string GetNickName()
    {
        return _nickName;
    }

    public void ResetStatuses()
    {

        // all statuses 
        hunger = FULL;
        thirst = FULL;
        happiness = FULL;
        health = FULL;
        fatigue = FULL;
        bladderCapacity = FULL;
        boredom = FULL;
    }

    public void Update()
    {
        if (_petAgeTimer.GetTimeLeft() <= 0)
        {
            AgePet();
            _petAgeTimer.ResetTimer();
            _petAgeTimer.timer = 10f;

        }

        if (false)
            Debug.Log("TimeLeft: " + _petAgeTimer.GetTimeLeft() + " Stoptime: " + _petAgeTimer._stopTime + " isTimeup: " + _petAgeTimer._timeUp);

        if (false)
            Debug.Log("hunger: " + hunger + " thirst: " + thirst + " happiness: " + happiness + " fatigue: " + fatigue + "\nbladder: " + bladderCapacity + " boredom: " + boredom
            + " health: " + health);
    }

    public void AgePet()
    {
        // depletes values based on specific rates
        hunger -= _hungerDepletionRate;
        thirst -= _thirstDepletionRate;
        happiness -= _happinessDepletionRate;
        fatigue -= _fatigueDepeletionRate;
        bladderCapacity -= _bladderCapacityDepletionRate;
        boredom -= _boredomDepletionRate;
        CalculateHealth();

        float tiredness = fatigue / FULL;
        // NOTE: had to swap values in animator for this logic to work
        _anim.SetFloat("Tired", (float)fatigue);
    }

    public void CalculateHealth()
    {
        // calculates the average as a float then casts it to an integer after moving
        // two decimal places
        float numTraits = 6f;
        float healthAVG = (float)(hunger + thirst + happiness + fatigue + bladderCapacity + boredom) / ((float)(FULL) * numTraits);
        health = (int)(healthAVG * FULL);
    }
    public void Start()
    {
        ResetStatuses();
        SetandReturnOutfitSystem();
        _anim = GetComponent<Animator>();
        // sets up fox's preferences
        _foxPrefs = new AnimalPreferences(70, 10, 50, 75, 40, 15, 3);
        _petAgeTimer = gameObject.AddComponent<Timer>();
        // begin aging process
        _petAgeTimer.SetTimer(TimeLapseRate);
        _petAgeTimer.PauseUnPause();
    }

    // this function only needs to be called once
    public OutfitChange SetandReturnOutfitSystem()
    {
        _outfitSystem = FindObjectOfType<OutfitChange>();
        return _outfitSystem;
    }

    public void RotateThroughFits()
    {
        _outfitSystem.ChangeOutfit(0, true);
    }

    public void ChangeIntoSpecificFit(int fitIndex)
    {
        _outfitSystem.ChangeOutfit(fitIndex, false);
    }


    public void FeedAnimal(IFood food)
    {
        // animate
        Eat(3f);
        // handles food values
        this._hunger += food.calories;
        this._thirst += food.rehydration;
        // handles pet preferences
        int pleasureAmnt = 0;
        // what the pet wants - what they get = pleasure level
        // NOTE: pleasureAmnt could be negative
        pleasureAmnt += food.fishey - _foxPrefs.fishPref;
        pleasureAmnt += food.meaty - _foxPrefs.meatPref;
        pleasureAmnt += food.watery - _foxPrefs.wateryPref;
        pleasureAmnt += food.sweet - _foxPrefs.sweetPref;
        // adds to pets happiness
        happiness += pleasureAmnt;
        // bladder effects
        bladderCapacity -= (int)((food.calories + food.rehydration) / 2);
        CalculateHealth();
    }

    public void PlayWithAnimal(IToy toy)
    {
        // handles toy values
        this._boredom += toy.funLevel;
        // handles pet preferences
        int pleasureAmnt = 0;
        // what the pet wants - what they get = pleasure level
        pleasureAmnt += toy.bouncy - _foxPrefs.bouncePref;
        pleasureAmnt += toy.smooth - _foxPrefs.smoothPref;
        pleasureAmnt += toy.squishey - _foxPrefs.squishPref;
        // adds to pets happiness
        happiness += pleasureAmnt;
        // fatigue effects
        fatigue -= (int)(toy.funLevel / 2);
        CalculateHealth();
    }


    public void SayGoodbye()
    {
        _anim.SetTrigger("Bye");
    }
    public void SayGoodbye(float byeTime)
    {
        SayGoodbye();
        StartCoroutine(WaitAndStopSayingGoodbye(byeTime));
    }
    public void StopSayingGoodbye()
    {
        _anim.SetTrigger("DoneBye");
    }
    IEnumerator WaitAndStopSayingGoodbye(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StopSayingGoodbye();
    }


    public void Putup()
    {
        _anim.SetTrigger("Putup");
    }
    public void PutDown()
    {
        _anim.SetTrigger("PutDown");
    }
    public void Putup(float upTime)
    {
        Putup();
        StartCoroutine(WaitAndPutDown(upTime));
    }
    IEnumerator WaitAndPutDown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PutDown();
    }


    public void Talk()
    {
        _anim.SetTrigger("Talk");
    }
    public void Talk(float convoTime)
    {
        _anim.SetTrigger("Talk");
        StartCoroutine(WaitAndStopTalking(convoTime));
    }
    public void StopTalking()
    {
        _anim.SetTrigger("DoneTalk");
    }
    IEnumerator WaitAndStopTalking(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StopTalking();
    }


    public void TakeUp()
    {
        _anim.SetTrigger("TakeUp");
    }
    public void TakeUp(float upTime)
    {
        TakeUp();
        StartCoroutine(WaitAndTakeDown(upTime));
    }
    public void TakeDown()
    {
        _anim.SetTrigger("TakeDown");
    }
    IEnumerator WaitAndTakeDown(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        TakeDown();
    }


    public void Eat()
    {
        _anim.SetTrigger("Eat");
    }
    public void Eat(float eatTime)
    {
        Eat();
        StartCoroutine(WaitAndStopEating(eatTime));
    }
    public void StopEating()
    {
        _anim.SetTrigger("DoneEating");
    }
    IEnumerator WaitAndStopEating(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StopEating();
    }


    public void Sleep()
    {
        _anim.SetTrigger("Sleep");
    }
    public void Sleep(float amnt)
    {
        _anim.SetFloat("Tired", .6f);
        Sleep();
        StartCoroutine(WaitAndStopSleeping(amnt));
    }
    public void StopSleeping()
    {
        _anim.SetTrigger("DoneSleeping");
    }
    IEnumerator WaitAndStopSleeping(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        StopSleeping();
    }
}
