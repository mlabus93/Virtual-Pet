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
        // keeps track of how many enemies there are in the scene
        private EnemyMovement[] _numEnemies;
        private EnemyManager _enemyManager;
        public bool _gameOver = false;

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

            _enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        }     
        // Use this for initialization
        void Start()
        {
            // makes sure there is a game manager present
            // Note: should be present in all World_* scripts
            if (FindObjectOfType<AnimalGameManager>() == null)
            {
                //_gameManager = gameObject.AddComponent<AnimalGameManager>();
                GameObject newManager = new GameObject();
                newManager.name = "manager";
                newManager = Instantiate(newManager) as GameObject;
                newManager.AddComponent<AnimalGameManager>();
                _gameManager = newManager.GetComponent<AnimalGameManager>();
                _gameManager.Load();
            }
            else
            {
                _gameManager = FindObjectOfType<AnimalGameManager>().GetComponent<AnimalGameManager>();
            }
            // instantiates player at spawnpoint
            _gameManager.InstantiatePlayer();
            _gameManager.PlayerAnimalObject.transform.position = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.position;
            // slows player down to playable speed
            _gameManager.PlayerAnimalObject.GetComponent<Player1StickMovement>()._speed = .15f;

            // spawns Ui elements
            _uiSpawner = gameObject.AddComponent<UISpawner>();
            _uiSpawner.SpawnUI();
            // assigns Ui elements
            World_MiniGame_01_UI _buttonSetup = gameObject.AddComponent<World_MiniGame_01_UI>();
            _buttonSetup.SetupButtons();
            _buttonSetup.SetupPanels();
        }

        public void Quit()
        {
            foreach (AudioSource src in FindObjectsOfType<AudioSource>())
            {
                src.Stop();
            }
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
            if (text != null)
            {
                if (!_gameOver)
                    text.text = "Score: " + _score + "\tTime: " + (int)_timer.timer;
                // if times up
                if ((int)_timer.timer <= 0)
                    OnGameOver();
            }
            else
            {
                text = GameObject.Find("ScoreText").GetComponent<Text>();
            }

         
            // level complete
            if (_enemyManager._swarmComplete && _timer.timer > 0 && _numEnemies.Length == 0)
            {
                OnLevelComplete(); 
            }
        }

        void OnLevelComplete()
        {
            Debug.Log("LEVEL UP");
            // Give player health
            _gameManager.PlayerAnimalObject.GetComponent<PlayerHealth>().currentHealth += 40;
            _gameManager.PlayerAnimalObject.GetComponent<PlayerHealth>().UpdateHealthSlider();
            // resets timer
            _timer.ResetTimer();
            //_timer._stopTime += 5;
            _timer.SetTimer(_timer._stopTime += 5f);
            _timer.ResetTimer();
            // Player gets +100 to score
            _score += 100;
            // enemy manager resets with increased difficulty
            _enemyManager.NextLevel();
        }

        void FixedUpdate()
        {
            _numEnemies = GameObject.FindObjectsOfType<EnemyMovement>();

            Debug.Log("There are: " + _numEnemies.Length + " in the scene");
            //_timer._stopTime -= Time.deltaTime;
        }

        public override void OnGameOver()
        {
            text.text = "Game Over!!";
            text.fontSize = 50;
            _alreadyGavePoints = true;
            _gameOver = true;

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

