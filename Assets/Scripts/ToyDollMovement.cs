using UnityEngine;

public class ToyDollMovement : MonoBehaviour {
    Transform player;
    DollHealth dollHealth;
    NavMeshAgent nav;
    bool isPlaying;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dollHealth = GetComponent<DollHealth>();
        nav = GetComponent<NavMeshAgent>();
    }

    public void PlayWithDoll()
    {
        dollHealth.currentHealth = 50;
        isPlaying = true;
        nav.Resume();
    }

    void Update()
    {
        if (dollHealth.currentHealth > 0 && isPlaying)
        {
            nav.SetDestination(player.position);
        }
        else
        {
            nav.Stop();
            isPlaying = false;
        }
    }
}
