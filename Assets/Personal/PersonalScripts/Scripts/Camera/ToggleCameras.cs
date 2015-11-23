﻿using UnityEngine;
using System.Collections;

public class ToggleCameras : MonoBehaviour {
    public Camera[] cameras = new Camera[9];
    private Camera lastCamera = new Camera();

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

    public void ToggleOverheadCameraView()
    {
        lastCamera = GetCurrentCamera();
        SwitchCameras(CameraPosition.Overhead);
    }

    public void ToggleSecurityCameraView()
    {
        if (GetCamPosition(lastCamera) == CameraPosition.Overhead)
        {
            SwitchCameras(CameraPosition.LivingRoom1);
        }
        else
        {
            SwitchCameras(GetCamPosition(lastCamera));
        }
    }

    public void LivingRoomCam()
    {
        SwitchCameras(CameraPosition.LivingRoom1);
    }

    public void ToyRoomCam()
    {
        SwitchCameras(CameraPosition.ToyRoom1);
    }

    public void BedRoomCam()
    {
        SwitchCameras(CameraPosition.BedRoom1);
    }

    public void BathRoomCam()
    {
        SwitchCameras(CameraPosition.BathRoom);
    }

    public void SwitchCameras(CameraPosition desiredCam)
    {
        foreach (Camera camera in cameras)
        {
            if (camera != null && (desiredCam != GetCamPosition(camera)))
            {
                // turn camera off
                camera.enabled = false;
                camera.tag = "Untagged";
            }
            else
            {
                // turn camera on
                camera.enabled = true;
                camera.tag = "MainCamera";
            }
        }
    }

    private Camera GetCurrentCamera()
    {
        return Camera.main;
    }

    private CameraPosition GetCamPosition(Camera currentCam)
    {
        return (CameraPosition)System.Enum.Parse(typeof(CameraPosition), currentCam.name);
    }

    public void ChangeCameraRoom()
    {
        Camera currentCam = GetCurrentCamera();
        CameraPosition currentCamPos = (CameraPosition)System.Enum.Parse(typeof(CameraPosition), currentCam.name);

        switch(currentCamPos)
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

    // Will need to add it to button later, but currently no collider calls it
    public void ChangeRooms(GameObject doorway)
    {
        Room currentRoom = 0;
        Room destinationRoom = (Room)System.Enum.Parse(typeof(Room), doorway.tag);

        Camera currentCam = Camera.main;
        CameraPosition currentCamPos = GetCamPosition(currentCam);

        switch (currentCamPos)
        {
            case CameraPosition.BathRoom:
                currentRoom = Room.BathRoom;
                break;
            case CameraPosition.BedRoom1:
            case CameraPosition.BedRoom2:
                currentRoom = Room.BedRoom;
                break;
            case CameraPosition.LivingRoom1:
            case CameraPosition.LivingRoom2:
                currentRoom = Room.LivingRoom;
                break;
            case CameraPosition.ToyRoom1:
            case CameraPosition.ToyRoom2:
                currentRoom = Room.ToyRoom;
                break;
            case CameraPosition.FirstPerson:
            //TODO find ROom of the player then set room
            default:
                break;
        }
        SetCamera(destinationRoom, currentRoom);
    }

    private void SetCamera(Room destinationRoom, Room currentRoom)
    {

        switch (currentRoom)
        {
            case Room.BathRoom:
                SwitchCameras(CameraPosition.BedRoom1);
                break;
            case Room.BedRoom:

                if (destinationRoom == Room.BedRoom)
                {
                    SwitchCameras(CameraPosition.LivingRoom1);
                }
                else
                {
                    SwitchCameras(CameraPosition.BathRoom);
                }
                break;
            case Room.LivingRoom:

                if (destinationRoom == Room.ToyRoom)
                {
                    SwitchCameras(CameraPosition.ToyRoom1);
                }
                else
                {
                    SwitchCameras(CameraPosition.BedRoom1);
                }
                break;
            case Room.ToyRoom:
                SwitchCameras(CameraPosition.LivingRoom1);
                break;
            default:
                break;
        }




    }

}