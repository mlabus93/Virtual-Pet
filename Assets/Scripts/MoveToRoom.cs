using UnityEngine;
using System.Collections;

public class MoveToRoom : MonoBehaviour {
    public GameObject cameraObject;

    void OnMouseDown()
    {
            cameraObject.GetComponent<ToggleCameras>().ChangeRooms(gameObject);
    }
}
