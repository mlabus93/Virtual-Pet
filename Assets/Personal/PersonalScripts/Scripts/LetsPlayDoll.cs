using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class LetsPlayDoll : MonoBehaviour
    {
        GameObject player;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void OnMouseDown()
        {
            player.GetComponent<MoveToAction>().PlayWithDoll();

        }
    }
}

