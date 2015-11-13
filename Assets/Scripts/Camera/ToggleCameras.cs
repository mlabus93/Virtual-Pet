using UnityEngine;
using System.Collections;

public class ToggleCameras : MonoBehaviour {
    public GameObject[] cameras = new GameObject[8];

    enum CameraPosition
    {
        Overhead = 0,
        LivingRoom1,
        LivingRoom2,
        ToyRoom1,
        ToyRoom2,
        BedRoom1,
        BedRoom2,
        BathRoom
    };


    // Use this for initialization
    void Start()
    {
        switchCameras(0);
    }

    //TEMP CODE TO DEBUG CAMERAS
    void Update()
    {
        int keyPress;

        if(int.TryParse(Input.inputString, out keyPress))

        {
            switchCameras(keyPress);
        }

    }

    private void switchCameras(int keyNum)
    {
        for (int i = 0; i < cameras.Length - 1; i++)
        {
            if (cameras[i] != null && keyNum != i)
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
