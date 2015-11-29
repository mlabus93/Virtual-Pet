// Project: Pet Pals
// File: GoToBed.cs
// Modification History:
// Author           Date
// Mirvil           11/23/15

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


namespace PersonalScripts
{
    public class GoToBed : MonoBehaviour
    {
        GameObject player;

        void OnMouseDown()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (!EventSystem.current.IsPointerOverGameObject() && player != null)
            {
                player.GetComponent<MoveToAction>().GoToBed();
            }

        }
    }

}
