using UnityEngine;
using System.Collections;

public enum GameState { playing, gameover };
namespace PersonalScripts
{
    public class World_MiniGame_02 : LevelManager
    {

        public Transform platformPrefab;
        public Transform coinPrefab;
        public static GameState gameState;

        private Transform playerTrans;
        private float platformsSpawnedUpTo = 0.0f;
        private ArrayList platforms;
        private ArrayList fallinItems;
        private float nextPlatformCheck = 0.0f;
        AnimalGameManager _gameManager;

        void Awake()
        {
            playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

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
            _gameManager.PlayerAnimalObject.transform.position = GameObject.Find("StartPoint").transform.position;
            //_gameManager.PlayerAnimalObject.transform.parent = (GameObject.Find("Player").transform.parent);
            _gameManager.PlayerAnimalObject.transform.parent = (GameObject.Find("Player").transform);

            _gameManager.PlayerAnimalObject.GetComponent<Rigidbody>().isKinematic = true;
            //Destroy(GetComponent<Rigidbody>());
            platforms = new ArrayList();
            fallinItems = new ArrayList();

            SpawnPlatforms(25.0f);
            StartGame();
        }

        void StartGame()
        {
            Time.timeScale = 1.0f;
            gameState = GameState.playing;
        }

        public override void AddPoints(int Amount)
        {
            //World_MiniGame_02_UI.score += Amount;
            score += Amount;
        }

        public void GameOver()
        {
            _gameManager.PlayerAnimalObject.GetComponent<PlayerHealth>().playerAudio.Play();
            Time.timeScale = 0.0f; //Pause the game
            gameState = GameState.gameover;
            World_MiniGame_02_UI.SP.CheckHighscore();
            // prevents player from accruing points
            if (!_alreadyGavePoints)
            {
                _alreadyGavePoints = true;
                World_MiniGame_02_UI.score += score;
            }
            _gameManager.AddCoins(World_MiniGame_02_UI.score);
            FindObjectOfType<AudioSource>().Stop();
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
                if (_gameManager.PlayerAnimalObject.transform.localScale.x > 6.5f)
                    _gameManager.PlayerAnimalObject.transform.localScale -= new Vector3(.1F, .1F, .1F);
            }

            //Do we need to spawn new platforms yet? (we do this every X meters we climb)
            float playerHeight = playerTrans.position.y;
            if (playerHeight > nextPlatformCheck)
            {
                PlatformMaintenaince(); //Spawn new platforms
            }

            //Update camera position if the player has climbed and if the player is too low: Set gameover.
            float currentCameraHeight = transform.position.y;
            float newHeight = Mathf.Lerp(currentCameraHeight, playerHeight, Time.deltaTime * 10);
            if (playerTrans.position.y > currentCameraHeight)
            {
                transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
            }
            else
            {
                //Player is lower..maybe below the cameras view?
                if (playerHeight < (currentCameraHeight - 10))
                {
                    GameOver();
                }
            }

            //Have we reached a new score yet?
            if (playerHeight > World_MiniGame_02_UI.score)
            {
                World_MiniGame_02_UI.score = (int)playerHeight;
            }
        }

        void LateUpdate()
        {
            if (platforms.Count < 1)
                PlatformMaintenaince();

            int platCount = 0;
            foreach (Transform plat in platforms)
            {
                if (plat.position.y > playerTrans.position.y)
                    platCount += 1;
            }

            if (platCount < 4)
            {
                Debug.Log("THERE ARE NO MORE PLATFORMS");
                PlatformMaintenaince();
            }
        }



        void PlatformMaintenaince()
        {
            nextPlatformCheck = playerTrans.position.y + 10;

            //Delete all platforms below us (save performance)
            for (int i = platforms.Count - 1; i >= 0; i--)
            {
                Transform plat = (Transform)platforms[i];
                if (plat.position.y < (transform.position.y - 10))
                {
                    Destroy(plat.gameObject);
                    platforms.RemoveAt(i);
                }
            }

            for (int i = fallinItems.Count - 1; i >= 0; i-- )
            {
                Transform cn = (Transform)fallinItems[i];
                if (cn.position.y < (transform.position.y - 10))
                {
                    Destroy(cn.gameObject);
                    fallinItems.RemoveAt(i);
                }
            }

                //Spawn new platforms, 25 units in advance
                SpawnPlatforms(nextPlatformCheck + 25f);
            int chanceForCoin = Random.Range(0, 100);
            if (chanceForCoin < 50)
                SpawnCoins(nextPlatformCheck + 25);
        }

        void SpawnCoins(float upTo)
        {
            float spawnHeight = platformsSpawnedUpTo;
            while (spawnHeight <= upTo)
            {
                float x = Random.Range(-10.0f, 10.0f);
                Vector3 pos = new Vector3(x, spawnHeight, 12.0f);

                Transform plat = (Transform)Instantiate(coinPrefab, pos, Quaternion.identity);
                plat.localScale = new Vector3(5F, 5F, 5F);
                plat.Rotate(Vector3.up * Time.deltaTime, Space.World);

                platforms.Add(plat);

                spawnHeight += Random.Range(1.6f, 3.5f);
            }
            platformsSpawnedUpTo = upTo;
        }

        void SpawnPlatforms(float upTo)
        {
            float spawnHeight = platformsSpawnedUpTo;
            while (spawnHeight <= upTo)
            {
                float x = Random.Range(-10.0f, 10.0f);
                Vector3 pos = new Vector3(x, spawnHeight, 12.0f);

                Transform plat = (Transform)Instantiate(platformPrefab, pos, Quaternion.identity);
                platforms.Add(plat);

                spawnHeight += Random.Range(1.6f, 3.5f);
            }
            platformsSpawnedUpTo = upTo;
        }
    }
}

