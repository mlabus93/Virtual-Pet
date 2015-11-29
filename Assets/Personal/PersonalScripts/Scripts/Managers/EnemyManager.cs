using UnityEngine;
using System.Collections.Generic;

using PersonalScripts;
public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    //public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public List<GameObject> _enemies;
    public bool _swarmComplete = false;
    private SpawnProbability _spawnProb = new SpawnProbability();
    static int dollCount = 0;
    int maxDolls = 1;

    public class SpawnProbability
    {
        public int Hellephant;
        public int ZomBear;
        public int ZomBunny;

        private int elphantIndex;
        private int bearIndex;
        private int bunnyIndex;

        public List<int> possibleEnemies;

        public SpawnProbability()
        {
            possibleEnemies = new List<int>();
            Hellephant = 0;
            ZomBear = 30;
            ZomBunny = 70;

            elphantIndex = 0;
            bearIndex = 1;
            bunnyIndex = 2;
        }
        public void IncreaseDifficulty()
        {
            Hellephant += 5;
            ZomBear += 5;
            ZomBunny += 3;

            // cap likelihood at 100
            if (Hellephant > 100)
                Hellephant = 100;
            if (ZomBunny > 100)
                ZomBunny = 100;
            if (ZomBear > 100)
                ZomBear = 100;
        }

        public int GetEnemy()
        {
            int probableChoice = Random.Range(0, 100);

            if (Hellephant >= probableChoice)
            {
                possibleEnemies.Add(elphantIndex);
            }
            if (ZomBear >= probableChoice)
            {
                possibleEnemies.Add(bearIndex);
            }
            if (ZomBunny >= probableChoice)
            {
                possibleEnemies.Add(bunnyIndex);
            }

            int firstPick = Random.Range(0, possibleEnemies.Count) - 1;
            if (firstPick < 0)
                firstPick = 0;

            int finalPick = possibleEnemies[firstPick];
            return finalPick;
        }
    }

    void Start ()
    {
        GameObject playerBody = GameObject.FindGameObjectWithTag("Player");

        if (playerBody != null)
        {
            playerHealth = playerBody.GetComponent<PlayerHealth>();
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }  
        else
            InvokeRepeating("FindPlayer", 1f, 2f);
    }

    void FindPlayer()
    {
        GameObject playerBody = GameObject.FindGameObjectWithTag("Player");

        if (playerBody != null)
        {
            Debug.Log("FOUND PLAYER");
            playerHealth = playerBody.GetComponent<PlayerHealth>();
            CancelInvoke("FindPlayer");
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }
    }

    public void NextLevel()
    {
        // reset dollcount
        dollCount = 0;
        // increase difficulty
        _spawnProb.IncreaseDifficulty();
        _swarmComplete = false;
        maxDolls += 1;
    }

    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f || dollCount >= maxDolls)
        {
            _swarmComplete = true;
            Debug.Log("SWARM COMPLETE");
            return;
        }

        // spawn at random location
        int spawnPointIndex = Random.Range (0, spawnPoints.Length);
        // spawn random enemy
        
        Instantiate(_enemies[_spawnProb.GetEnemy()], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

        // swarm is not yet complete
        _swarmComplete = false;
       
        dollCount++;
    }
}
