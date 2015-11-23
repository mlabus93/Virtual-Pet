using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections;


namespace PersonalScripts
{
    public class Character : MonoBehaviour, IAnimalCharacter
    {
        private const int FULL = 100;
        private const int EMPTY = 0;
        protected string _nickName = "DEFAULT";

        // animal status traits and depletion rates
        protected AnimalPreferences _Prefs;
        protected int _hunger, _hungerDepletionRate = 5;
        protected int _thirst, _thirstDepletionRate = 5;
        protected int _happiness, _happinessDepletionRate = 5;
        protected int _fatigue, _fatigueDepeletionRate = 5;
        protected int _bladderCapacity, _bladderCapacityDepletionRate = 10;
        protected int _boredom, _boredomDepletionRate = 4;
        protected int _health; // NOTE: Health is calculated as an average of all of the other status fields


        protected void Fear(string nickName)
        {
            Debug.Log("Im so scared: " + nickName);
        }

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

        protected OutfitChange _outfitSystem;
        public Animator _anim;
        public Timer _petAgeTimer;
        public int _TimeLapseRate = 5; // rate at which animal traits deplete/ refresh in sec
        [SerializeField]
        public int TimeLapseRate { get { return _TimeLapseRate; } set { _TimeLapseRate = value; } }
        public WeaponHandler _weaponHandler;
        public void Attack(int attackType)
        {
            _weaponHandler.Attack(attackType);
        }
        public void DisableAllWeapons()
        {
            _weaponHandler.DisableAllWeapons();
        }
        public void ChangeWeapons()
        {
            _weaponHandler.ChangeWeapons();
        }
        public void ChangeWeapons(int index, bool loop = true)
        {
            _weaponHandler.ChangeWeapons(index, loop);
        }
        virtual public PlayableCharacters GetAnimalType()
        {
            return PlayableCharacters.Panda;
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
        public void ChangeHats()
        {
            _outfitSystem.ChangeHats();
        }
        public void ChangeHats(int index, bool loop = true)
        {
            _outfitSystem.ChangeHats(index, loop);
        }
        public void AdjustAgingRate(float ageRate)
        {
            _petAgeTimer.SetTimer(_petAgeTimer._stopTime + ageRate);
        }

        public float GetAgeRate()
        {
            return _petAgeTimer._stopTime;
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
            //_anim.SetFloat("Tired", 0f);
        }


        public void Update()
        {
            if (_petAgeTimer.GetTimeLeft() <= 0)
            {
                // only age pet if its in main game
                if (Application.loadedLevelName == "Main")
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
        public virtual void Start()
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

        // this function only needs to be called once
        public virtual OutfitChange SetandReturnOutfitSystem()
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
            pleasureAmnt += food.fishey - _Prefs.fishPref;
            pleasureAmnt += food.meaty - _Prefs.meatPref;
            pleasureAmnt += food.watery - _Prefs.wateryPref;
            pleasureAmnt += food.sweet - _Prefs.sweetPref;
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
            pleasureAmnt += toy.bouncy - _Prefs.bouncePref;
            pleasureAmnt += toy.smooth - _Prefs.smoothPref;
            pleasureAmnt += toy.squishey - _Prefs.squishPref;
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


}
