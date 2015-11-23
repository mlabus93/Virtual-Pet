using System;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

using System.Collections.Generic;
using System.Xml.Serialization;

namespace PersonalScripts
{
    public class AnimalGameManager : MonoBehaviour
    {
        // These are static because there should be only one game manager
        // and only one player
        public static AnimalGameManager _manager;
        public static IAnimalCharacter _player;
        public static int _coins = 500;
        public int _volumeLevel;
        public float _gameSpeed;
        public string _userName = "Temp";
        public GameObject PlayableAnimal;
        // saving utility
        AnimalContainer animalCollection = new AnimalContainer();
        GameContainer gameCollection = new GameContainer();


        public void TogglePauseGame()
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void AddCoins(int Amount)
        {
            _coins += Amount;
        }

        public void LoadXMLData()
        {
            // parses the xml data for gamesaves and character data
            LoadGameSave();
            LoadPetInfo();
        }

        public void SaveXMLData()
        {
            // saves relevant information from game and pet
            SaveGameSave();
            SavePetInfo();
        }

        public void AdjustVolume(float nwVolume)
        {
            _volumeLevel = (int)nwVolume;
            // Volume must be between 0 and 1
            AudioListener.volume = (float)(.01) * nwVolume;
        }

        public void AdjustGameSpeed(float nwGameSpeed)
        {
            _player.AdjustAgingRate(nwGameSpeed);
            _gameSpeed = _player.GetAgeRate();
        }
        private void SaveGameSave(int i = 0)
        {
            if (gameCollection.gameSaves.Count < 1)
            {
                // creates new game
                gameCollection.gameSaves.Add(new GameSave(name, _volumeLevel, _coins));
            }
            else
            {
                // saves to given index, defaulted to 0
                gameCollection.gameSaves[i].CoinAmount = _coins;
                gameCollection.gameSaves[i].VolumeLevel = _volumeLevel;
                gameCollection.gameSaves[i].GameSpeed = _gameSpeed;
            }
            gameCollection.Save(Path.Combine(Application.persistentDataPath, "Gamenfo.xml"));
        }

        private void SavePetInfo(int i = 0)
        {
            if (animalCollection.animals.Count < 1)
            {
                // creates new animal
                animalCollection.animals.Add(new Animal(_player.GetNickName()));
            }
            else
            {
                // saves to given index, defaulted to 0
                // saves animals species and nickname
                animalCollection.animals[i].species = _player.GetAnimalType();
                animalCollection.animals[i].Name = _player.GetNickName();
                // saves animals outfit
                animalCollection.animals[i]._playerFit.EyeIndex = _player.SetandReturnOutfitSystem().GetCurrentEyeSelected();
                animalCollection.animals[i]._playerFit.OutFitIndex = _player.SetandReturnOutfitSystem().GetCurrentOutfitIndex();
                // saves animal statuses
                animalCollection.animals[i]._playerStats.Happiness = _player.happiness;
                animalCollection.animals[i]._playerStats.Health = _player.health;
                animalCollection.animals[i]._playerStats.Hunger = _player.hunger;
                animalCollection.animals[i]._playerStats.Thirst = _player.thirst;
                animalCollection.animals[i]._playerStats.Fatigue = _player.fatigue;
                animalCollection.animals[i]._playerStats.Boredom = _player.boredom;
                animalCollection.animals[i]._playerStats.BladderCapacity = _player.bladderCapacity;
                // saves animal location
                animalCollection.animals[i]._playerLoci.xpos = _player.GetAnimalPosition().x;
                animalCollection.animals[i]._playerLoci.ypos = _player.GetAnimalPosition().y;
                animalCollection.animals[i]._playerLoci.zpos = _player.GetAnimalPosition().z;
            }
            animalCollection.Save(Path.Combine(Application.persistentDataPath, "Animalnfo.xml"));
        }
        private void LoadGameSave(int i = 0)
        {
            // loads gamedata from given index, defaulted to 0
            GameContainer gme = GameContainer.Load(Path.Combine(Application.persistentDataPath, "Gamenfo.xml"));
            if (gme != null)
            {
                _coins = gme.gameSaves[i].CoinAmount;
                _volumeLevel = gme.gameSaves[i].VolumeLevel;
                _gameSpeed = gme.gameSaves[i].GameSpeed;
                AdjustGameSpeed(_gameSpeed);
            }

        }

        private void LoadPetInfo(int i = 0)
        {
            AnimalContainer ac = AnimalContainer.Load(Path.Combine(Application.persistentDataPath, "Animalnfo.xml"));
            if (ac != null)
            {
                // sets player status info
                _player.hunger = ac.animals[i]._playerStats.Hunger;
                _player.thirst = ac.animals[i]._playerStats.Thirst;
                _player.happiness = ac.animals[i]._playerStats.Happiness;
                _player.fatigue = ac.animals[i]._playerStats.Fatigue;
                _player.bladderCapacity = ac.animals[i]._playerStats.BladderCapacity;
                _player.boredom = ac.animals[i]._playerStats.Boredom;
                _player.health = ac.animals[i]._playerStats.Health;

                // sets player location info
                _player.SetAnimalPosition(new Vector3(ac.animals[i]._playerLoci.xpos, ac.animals[i]._playerLoci.ypos, ac.animals[i]._playerLoci.zpos));

                // sets player outfit info
                _player.SetandReturnOutfitSystem().ChangeOutfit(ac.animals[i]._playerFit.OutFitIndex, false);
                if (_player.SetandReturnOutfitSystem().GetCurrentEyeSelected() != ac.animals[i]._playerFit.EyeIndex)
                    _player.SetandReturnOutfitSystem().ChangeEyes();

                // TODO: set weapon information
            }

        }
        public void SaveAnimal(string path)
        {
            var serializer = new XmlSerializer(typeof(AnimalGameManager));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
        }

        public static AnimalGameManager LoadAnimal(string path)
        {
            var serializer = new XmlSerializer(typeof(AnimalGameManager));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as AnimalGameManager;
            }
        }

        //Loads the xml directly from the given string. Useful in combination with www.text.
        public static AnimalGameManager LoadFromText(string text)
        {
            var serializer = new XmlSerializer(typeof(AnimalGameManager));
            return serializer.Deserialize(new StringReader(text)) as AnimalGameManager;
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
            // clears player reference
            _player = null;

        }

        public void FindExactAnimal()
        {
            // determines Player animal type, there should only be 1 player
            // there could possibly be more than 1 animal
            GameObject holder = GameObject.FindGameObjectWithTag("Player");

            // if-else structure to determine exact animal type
            if (_player == null)
            {
                _player = holder.GetComponent<CatCharacter>();
                if (_player == null)
                {
                    _player = holder.GetComponent<DogCharacter>();
                    if (_player == null)
                    {
                        _player = holder.GetComponent<RabbitCharacter>();
                        if (_player == null)
                        {
                            _player = holder.GetComponent<FoxCharacter>();
                            if (_player == null)
                            {
                                _player = holder.GetComponent<PenguinCharacter>();
                                if (_player == null)
                                {
                                    _player = holder.GetComponent<PandaCharacter>();
                                    // if player has not been assigned by this point there was an error
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("NO PLAYER OBJECT FOUND!");
            }

            Debug.Log(_player.GetNickName());
        }
        public void LateUpdate()
        {
            //FindExactAnimal();
            Debug.Log(_player.GetNickName());
        }

        public void Save()
        {

            FileStream file;
            // creates file reader in binary format
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                // NEW GAME
                file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
            }
            Debug.Log("Now Saving to " + Application.persistentDataPath + "/playerInfo.dat");
            // creates new data object with values from player's current state
            // *must be written this way in order for it to be serialized
            PlayerData data = new PlayerData(_player.hunger, _player.thirst,
                _player.happiness, _player.health, _player.fatigue, _player.bladderCapacity);
            // writes to binary file and closes
            bf.Serialize(file, data);
            file.Close();

            SaveXMLData();
        }

        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                // if file exists read it into the Player object
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            }
            LoadXMLData();
        }
    }

    [Serializable]
    class GameData
    {

        public int _petSelection;
        public PlayableCharacters _playerSpecies;

        public GameData(int vol, PlayableCharacters species)
        {
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
}
