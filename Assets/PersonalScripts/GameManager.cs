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
    public static IAnimalCharacter _player;
    public static int _coins;

    PlayableCharacters characterSelection;
    
    public void AddCoins(int Amount)
    {
        _coins += Amount;
    }
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
        //_player = GameObject.FindObjectOfType<CatCharacter>();
    }

    public void Save()
    {
        
        // creates file reader in binary format
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
        // creates new data object with values from player's current state
        // *must be written this way in order for it to be serialized
        PlayerData data = new PlayerData(_player.hunger, _player.thirst, 
            _player.happiness, _player.health, _player.fatigue, _player.bladderCapacity);
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
class GameData
{
    public int _volumeLevel;
    public int _musicLevel;
    public int _petSelection;
    public PlayableCharacters _playerSpecies;

    public GameData(int vol, int musiclv, PlayableCharacters species)
    {
        _volumeLevel = vol;
        _musicLevel = musiclv;
        _playerSpecies = species;
    }
}


[Serializable]
class PlayerData
{
    public int _hunger;
    public int _thirst;
    public int _happiness;
    public int _health;
    public int _fatigue;
    public int _bladder;
    public PlayerData(int hunger, int thirst, int happiness, int health, int fatigue, int bladder)
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