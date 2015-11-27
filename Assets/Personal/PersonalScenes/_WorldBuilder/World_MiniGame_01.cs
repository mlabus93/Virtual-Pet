using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[RequireComponent(typeof(AnimalGameManager))]
namespace PersonalScripts
{
    public class World_MiniGame_01 : LevelManager
    {
        private UISpawner _uiSpawner;
        AnimalGameManager _gameManager;
        Text text;                      // Reference to the Text component.
        public Timer _timer;                   // The time until the level is complete.


        // TODO: consolodate Awake into Start
        public override void Awake()
        {
            // Set up the reference
            text = GetComponent<Text>();

            // Reset the score.
            _score = 0;
            // create new timer
            _timer = gameObject.AddComponent<Timer>();
            _timer.SetTimer(_levelLength);
            _timer._isPaused = false;

        }     
        // Use this for initialization
        void Start()
        {
            // makes sure there is a game manager present
            // Note: should be present in all World_* scripts
            if (FindObjectOfType<AnimalGameManager>() == null)
            {
                _gameManager = gameObject.AddComponent<AnimalGameManager>();
            }
            else
            {
                _gameManager = FindObjectOfType<AnimalGameManager>().GetComponent<AnimalGameManager>();
            }
            // instantiates player at spawnpoint
            _gameManager.InstantiatePlayer();
            _gameManager.PlayerAnimalObject.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;


            // spawns Ui elements
            _uiSpawner = gameObject.AddComponent<UISpawner>();
            _uiSpawner.SpawnUI();
            // assigns Ui elements
            World_MiniGame_01_UI _buttonSetup = gameObject.AddComponent<World_MiniGame_01_UI>();
            _buttonSetup.SetupButtons();
        }

        void Update()
        {
            if (_gameManager.PlayerAnimalObject != null)
            {
                // shrinks player to appropriate size
                if (_gameManager.PlayerAnimalObject.transform.localScale.x > 3.5f)
                    _gameManager.PlayerAnimalObject.transform.localScale -= new Vector3(.1F, .1F, .1F);
            }

            // Set the displayed text to be the word "Score" followed by the score value.
            //if (_timer._timeUp && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth > 0)
            if (text != null)
            {
                text.text = "Score: " + _score + "\tTime: " + (int)_timer._stopTime;
                if ((int)_timer._stopTime <= 0)
                    OnGameOver();
            }
        }



        void FixedUpdate()
        {
            _timer._stopTime -= Time.deltaTime;
        }

        public override void OnGameOver()
        {
            text.text = "Game Over!!";
            _alreadyGavePoints = true;

            // checks if there is a manager in the scene
            if (_gameManager == null)
                _gameManager = FindObjectOfType<AnimalGameManager>();
            // if there isn't then we add one
            if (_gameManager == null)
                _gameManager = gameObject.AddComponent<AnimalGameManager>();

            // handles glitch where player consistently gets points before returning
            // to main game
            if (!_alreadyGavePoints)
                _gameManager.AddCoins(_score);
            // save points
            _gameManager.Save();
        }

    }
}

