using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class LetsPlayBall : MonoBehaviour
    {
        GameObject player;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnMouseDown()
        {
            player.GetComponent<MoveToAction>().PlayWithBall();

        }

        void OnTriggerEnter(Collider other)
        {

            if (other.gameObject == player && player.GetComponent<MoveToAction>().moveRandom)
            {
                player.GetComponent<MoveToAction>().PlayWithBall();
            }
        }
    }
}

