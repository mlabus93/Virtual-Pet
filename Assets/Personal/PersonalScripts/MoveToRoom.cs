// Project: Pet Pals
// File: MoveToRoom.cs
// Modification History:
// Author           Date
// Mirvil           11/23/15

using System.Collections;
using UnityEngine.EventSystems;

public class MoveToRoom : MonoBehaviour {
    public GameObject cameraObject;

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject()){
            cameraObject.GetComponent<ToggleCameras>().ChangeRooms(gameObject);
        }
    }
}
