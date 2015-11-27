using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class World_MainGame : MonoBehaviour
    {

        private UISpawner _uiSpawner;
        AnimalGameManager _manager;
        // Use this for initialization
        void Start()
        {
            // makes sure there is a game manager present
            // Note: should be present in all World_* scripts
            if (FindObjectOfType<AnimalGameManager>() == null)
            {
                _manager = gameObject.AddComponent<AnimalGameManager>();
            }
            else
            {
                _manager = FindObjectOfType<AnimalGameManager>().GetComponent<AnimalGameManager>();
            }
            // instantiates player at spawnpoint
            _manager.InstantiatePlayer();
            _manager.PlayerAnimalObject.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;

            Debug.Log("before load");
            PrintLoadedData();
            _manager.Load();
            Debug.Log("after load");
            PrintLoadedData();
            

            StartCoroutine("BeginAi", 1f);
            // spawns Ui elements
            _uiSpawner = gameObject.AddComponent<UISpawner>();
            _uiSpawner.SpawnUI();
            // assigns Ui elements
            World_MainGame_UI _buttonSetup = gameObject.AddComponent<World_MainGame_UI>();
            _buttonSetup.SetupButtons();
        }

        void BeginAi()
        {
            Debug.Log("BEGIN AI INVOKED");
            //enable components instead of adding new ones. This breaks touchmovement otherwise
            _manager.PlayerAnimalObject.GetComponent<NavMeshAgent>().enabled = true;
            _manager.PlayerAnimalObject.GetComponent<MoveToAction>().enabled = true;
        }

        void PrintLoadedData()
        {
            //AnimalGameManager._player.ChangeWeapons();
            //Debug.Log("_playerWeapon in world gen: " + AnimalGameManager._player.);
        }

    }
}