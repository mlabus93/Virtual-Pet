using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


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
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                player.GetComponent<MoveToAction>().GoToBed();
            }

        }
    }

}
