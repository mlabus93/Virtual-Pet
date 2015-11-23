using UnityEngine;
using System.Collections;


namespace PersonalScripts
{
    public class GoToBed : MonoBehaviour
    {
        GameObject player;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnMouseDown()
        {
            player.GetComponent<MoveToAction>().GoToBed();

        }

        void OnTriggerExit(Collider other)
        {

            if (other.gameObject == player && player.GetComponent<MoveToAction>().moveRandom)
            {
                player.GetComponent<MoveToAction>().MoveRandomly();
            }
        }
    }

}
