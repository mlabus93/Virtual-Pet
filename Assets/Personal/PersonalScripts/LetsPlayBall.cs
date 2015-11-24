using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                player.GetComponent<MoveToAction>().PlayWithBall();
            }
        }
    }
}
