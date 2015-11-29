// Project: Pet Pals
// File: Timer.cs
// Modification History:
// Author           Date
// Jean-Baptiste    11/22/15
// Jean-Baptiste    11/28/15

using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    private float _timer;
    public float timer { get { return _timer; } set { _timer = value; } }                   // actual timer itself
    public float _stopTime { get; set; }              // starting value of timer
    public bool _timeUp;
    public bool _isPaused { get; set; }

    public void ResetTimer()
    {
        this.timer = this._stopTime;
        this._timeUp = false;
    }

    public void SetTimer(float amnt)
    {
        this._stopTime = amnt;
        this.timer = amnt;
    }

    public void AddTime(float amnt)
    {
        this._stopTime += amnt;
    }

    public int GetTimeLeft()
    {
        return (int)this.timer;
    }

    public Timer (float time)
    {
        this.SetTimer(time);
    }

    public void PauseUnPause()
    {
        this._isPaused = !this._isPaused;
    }


    public void ResolvedUpdate()
    {
        if (timer > 0)
        {
            if (!this._isPaused)
                this.timer -= Time.deltaTime;
            this._timeUp = false;
        }
        else
        {
            this._timeUp = true;
        }
    }
	// Use this for initialization
	void Awake () {
        // NOTE: after instantiation, timer needs to be unpaused
        this._isPaused = true;
	}
	
    void FixedUpdate()
    {
        // Timer doesn't need to drop if its negative
        if (timer > 0)
        {
            if (!this._isPaused)
                this.timer -= Time.deltaTime;
            this._timeUp = false;
        }
        else
        {
            this._timeUp = true;
        }
    }

    void Update()
    {
        if (this._timer <= 0)
            this._timeUp = true;
    }
    
}
