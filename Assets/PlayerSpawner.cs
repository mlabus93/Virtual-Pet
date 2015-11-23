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
            SpawnPlayer();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SpawnPlayer()
    {
        Debug.Log(AnimalGameManager._player.GetNickName());
        if (AnimalGameManager._player.GetNickName() == "Ms.Fox")
        {
            // instantiates animal prefab into scene
            GameObject playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Fox", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
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
            GameObject playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Dog", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            playerClone.name = "Instantiated Player from Resources";
            // loads its values
            _manager.Load();

            // moves it to this objects location
            playerClone.transform.position = transform.position;
            AnimalGameManager._player = playerClone.GetComponent<DogCharacter>();
            _manager.Save();
        }
        if (AnimalGameManager._player.GetNickName() == "Mr.Penguin")
        {
            // instantiates animal prefab into scene
            GameObject playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Penguin", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
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
            GameObject playerClone = Instantiate(Resources.Load("PlayableCharacters/Mr.Cat", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
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
            GameObject playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Rabbit", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
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
            GameObject playerClone = Instantiate(Resources.Load("PlayableCharacters/Ms.Panda", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            playerClone.name = "Instantiated Player from Resources";

            // moves it to this objects location
            playerClone.transform.position = transform.position;
            AnimalGameManager._player = playerClone.GetComponent<PandaCharacter>();
            
            // loads its values
            _manager.Load();


            _manager.Save();
        }
    }
    }

}
