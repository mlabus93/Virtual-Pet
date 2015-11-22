using UnityEngine;
using System.Collections;

public class LetsPlayDoll : MonoBehaviour {
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnMouseDown()
    {
        player.GetComponent<MoveToAction>().PlayWithDoll();

    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject == player && player.GetComponent<MoveToAction>().moveRandom)
        {
            player.GetComponent<MoveToAction>().PlayWithDoll();
        }
    }
}
