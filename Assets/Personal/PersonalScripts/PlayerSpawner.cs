using UnityEngine;
using System.Collections;
////////////////////////////////////////////////////////////
///<summary>
/// Description: This script will find which species animal is saved
/// in the game manager and instantiate that breed, it will 
/// then load the last sdaved animal configurations to that animal
/// breed
/// </summary> 
///////////////////////////////////////////////////////////

namespace PersonalScripts
{
    public class PlayerSpawner : MonoBehaviour
    {
        // Playable animal characterssdfasd
        GameObject[] _characters;
        AnimalGameManager _manager;

        // Use this for initialization
        void Start()
        {
            _manager = FindObjectOfType<AnimalGameManager>();
            //SpawnPlayer();
        }


        public GameObject SpawnPlayer()
        {
            Debug.Log(AnimalGameManager._player.GetNickName());
            GameObject playerClone = new GameObject();
            if (AnimalGameManager._player.GetNickName() == "Ms.Fox")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Fox", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                _manager.Load();

                // moves it to this objects location
                playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<FoxCharacter>();
                _manager.Save();
            }
            if (AnimalGameManager._player.GetNickName() == "Mr.Doggy")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Dog", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                _manager.Load();

                // moves it to this objects location
                //playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<DogCharacter>();
                _manager.Save();
            }
            if (AnimalGameManager._player.GetNickName() == "Mr.Penguin")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Penguin", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                _manager.Load();

                // moves it to this objects location
                playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<PenguinCharacter>();
                _manager.Save();
            }
            if (AnimalGameManager._player.GetNickName() == "Mr.Kitty")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Cat", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                _manager.Load();

                // moves it to this objects location
                playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<CatCharacter>();
                _manager.Save();
            }
            if (AnimalGameManager._player.GetNickName() == "Mr.Rabbit")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Rabbit", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                _manager.Load();

                // moves it to this objects location
                playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<RabbitCharacter>();
                _manager.Save();
            }
            if (AnimalGameManager._player.GetNickName() == "Mr.Panda")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Panda", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";

                // moves it to this objects location
                //playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<PandaCharacter>();

                // loads its values
                _manager.Load();

                _manager.Save();
            }
            return playerClone;
        }

        public GameObject SpawnPlayer(string animalName, Vector3 loc)
        {
            //Debug.Log(AnimalGameManager._player.GetNickName());
            GameObject playerClone = new GameObject();

            if (animalName == "Ms.Fox")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Fox", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                //_manager.Load();

                // moves it to this objects location
                playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<FoxCharacter>();
                //_manager.Save();
            }
            if (animalName == "Mr.Doggy")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Dog", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                //_manager.Load();

                // moves it to this objects location
                //playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<DogCharacter>();
                //_manager.Save();
            }
            if (animalName == "Mr.Penguin")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Penguin", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                //_manager.Load();

                // moves it to this objects location
                playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<PenguinCharacter>();
                //_manager.Save();
            }
            if (animalName == "Mr.Kitty")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Cat", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                //_manager.Load();

                // moves it to this objects location
                playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<CatCharacter>();
                //_manager.Save();
            }
            if (animalName == "Mr.Rabbit")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Rabbit", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";
                // loads its values
                //_manager.Load();

                // moves it to this objects location
                playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<RabbitCharacter>();
                //_manager.Save();
            }
            if (animalName == "Mr.Panda")
            {
                // instantiates animal prefab into scene
                playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Panda", typeof(GameObject)), transform.position, Quaternion.identity) as GameObject;
                playerClone.name = "Instantiated Player from Resources";

                // moves it to this objects location
                //playerClone.transform.position = transform.position;
                AnimalGameManager._player = playerClone.GetComponent<PandaCharacter>();

                // loads its values
                //_manager.Load();

                //_manager.Save();
            }
            return playerClone;
        }
    }

}
