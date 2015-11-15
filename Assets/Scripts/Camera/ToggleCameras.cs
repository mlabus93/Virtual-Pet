using UnityEngine;
using System.Collections;

public class ToggleCameras : MonoBehaviour {
    public static GameObject[] cameras = new GameObject[8];

    // Use this for initialization
    void Start()
    {
        SwitchCameras(CameraPosition.LivingRoom1);
    }

    //TEMP CODE TO DEBUG CAMERAS
    void Update()
    {
        int keyPress;

        if(int.TryParse(Input.inputString, out keyPress))

        {
            SwitchCameras((CameraPosition)keyPress);
        }

    }

    public static void SwitchCameras(CameraPosition keyNum)
    {
        for (int i = 0; i < cameras.Length - 1; i++)
        {
            if (cameras[i] != null && (int)keyNum != i)
            {
                // turn camera off
                cameras[i].GetComponent<Camera>().enabled = false;
            }
            else
            {
                // turn camera on
                cameras[i].GetComponent<Camera>().enabled = true;
            }
        }
    }

    void SelectCamera(CameraPosition position)
    {
    }
}
