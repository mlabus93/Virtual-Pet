// Project: Pet Pals
// File: LetsPlayDoll.cs
// Modification History:
// Author           Date
// Mirvil           11/23/15

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace PersonalScripts
{
    public class LetsPlayDoll : MonoBehaviour
    {
        GameObject player;

        void OnMouseDown()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (!EventSystem.current.IsPointerOverGameObject() && player != null)
            {
                player.GetComponent<MoveToAction>().PlayWithDoll();
            }
        }
    }
}
