using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class ToyDollMovement : MonoBehaviour
    {
        Transform player;
        DollHealth dollHealth;
        NavMeshAgent nav;
        Vector3 initialPosition;
        bool isPlaying;

        void BecomeAlive()
        {
            initialPosition = gameObject.transform.position;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            dollHealth = GetComponent<DollHealth>();
            nav = GetComponent<NavMeshAgent>();
        }

        public void PlayWithDoll()
        {
            BecomeAlive();
            dollHealth.currentHealth = 10;
            isPlaying = true;
            nav.Resume();
        }

        void Update()
        {
            if (player != null)
            {
                if (dollHealth.currentHealth > 0 && isPlaying)
                {
                    nav.SetDestination(player.position);
                }
                else
                {
                    nav.SetDestination(initialPosition);
                    isPlaying = false;
                }
            }
        }        
    }
}

