using UnityEngine;
using System.Collections;

public class LetsEat : MonoBehaviour{
    Transform player;

    void OnMouseDown()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        player.GetComponent<MoveToAction>().GoToFoodTable();
        //ToggleCameras.
    }
}
