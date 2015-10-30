using UnityEngine;
using System.Collections;

public class Animal 
{
    // animal status traits
    [SerializeField] public float _hunger { get; set; }
    [SerializeField] public float _thirst { get; set; }
    [SerializeField] public float _happiness { get; set; }
    [SerializeField] public float _health { get; set; }
    [SerializeField] public float _fatigue { get; set; }
    [SerializeField] public float _bladder { get; set; }

    void Start ()
    {
        _health = 100;
        _thirst = 100;
        _happiness = 100;
        _fatigue = 100;
        _bladder = 100;
        _hunger = 100;
    }



}
