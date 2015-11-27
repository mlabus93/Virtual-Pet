using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    static int dollCount = 0;

    int maxDolls = 10;

    void Start ()
    {
        GameObject playerBody = GameObject.FindGameObjectWithTag("Player");

        Debug.Log("made it past");
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

    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f || dollCount >= maxDolls)
        {
            //dollCount = 0;
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        dollCount++;
    }
}
