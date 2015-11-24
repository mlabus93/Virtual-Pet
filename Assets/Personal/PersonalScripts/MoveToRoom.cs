using UnityEngine;
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
