using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // These are static because there should be only one game manager
    // and only one player
    public static GameManager _manager;
    public static Animal _player;
    public static int _coins;

    void Awake()
    {
        // creates a singleton
        if (_manager == null)
        {
            DontDestroyOnLoad(gameObject);
            _manager = this;
        }
        else if (_manager != this)
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        // creates file reader in binary format
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        // creates new data object with values from player's current state
        // *must be written this way in order for it to be serialized
        PlayerData data = new PlayerData(_player._hunger, _player._thirst, 
            _player._happiness, _player._health, _player._fatigue, _player._bladder);
        // writes to binary file and closes
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            // if file exists read it into the Player object
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            
        }
    }
}

[Serializable]
class PlayerData
{
    public float _hunger;
    public float _thirst;
    public float _happiness;
    public float _health;
    public float _fatigue;
    public float _bladder;
    public PlayerData(float hunger, float thirst, float happiness, float health, float fatigue, float bladder)
    {
        _hunger = hunger;
        _thirst = thirst;
        _happiness = happiness;
        _health = health;
        _fatigue = fatigue;
        _bladder = bladder;
    }
    
    public void PrintStats()
    {
        Debug.Log("Hunger: " + _health + "\nThirst: " + _thirst + "\nHappiness: " + _happiness +
            "\nHealth: " + _health + "\nFatigue: " + _fatigue + "\nBladder: " + _bladder);
    }
}