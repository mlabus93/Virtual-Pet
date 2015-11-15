using UnityEngine;
using System.Collections;

public class ToggleCameras : MonoBehaviour {
    public GameObject[] cameras = new GameObject[8];

    // Use this for initialization
    void Start()
    {
        //TODO - Need to set the start cam to the room where the player is
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

    public void SwitchCameras(CameraPosition desiredCam)
    {
        for (int i = 0; i < cameras.Length - 1; i++)
        {
           

            if (cameras[i] != null && (int)desiredCam != i)
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

    void ChangeCameraRoom()
    {
        CameraPosition currentCam = (CameraPosition)System.Enum.Parse(typeof(CameraPosition), Camera.current.tag);

        switch(currentCam)
        {
            case CameraPosition.BedRoom1:
                SwitchCameras(CameraPosition.BedRoom2);
                break;
            case CameraPosition.BedRoom2:
                SwitchCameras(CameraPosition.BedRoom1);
                break;
            case CameraPosition.ToyRoom1:
                SwitchCameras(CameraPosition.ToyRoom2);
                break;
            case CameraPosition.ToyRoom2:
                SwitchCameras(CameraPosition.ToyRoom1);
                break;
            case CameraPosition.LivingRoom1:
                SwitchCameras(CameraPosition.LivingRoom2);
                break;
            case CameraPosition.LivingRoom2:
                SwitchCameras(CameraPosition.LivingRoom1);
                break;
             // For when I added a camera for first person (may not be used)
            //case CameraPosition.Overhead:
            //    ToggleCameras.SwitchCameras(CameraPosition.FirstPerson);
            //    break;
            //case CameraPosition.FirstPerson:
            //    ToggleCameras.SwitchCameras(CameraPosition.Overhead);
            //    break;
            default:
                break;
        }


    }
}
