using UnityEngine;
using System.Collections;

public class CameraRoomSwitch : MonoBehaviour {
    public ToggleCameras cameraToggle;


    void ChangeRooms(Collider doorway)
    {
        Room currentRoom = 0;
        Room destinationRoom = (Room)System.Enum.Parse(typeof(Room), doorway.tag);

        CameraPosition currentCam = (CameraPosition)System.Enum.Parse(typeof(CameraPosition), Camera.current.tag);

        switch (currentCam)
        {
            case CameraPosition.BathRoom:
                currentRoom = Room.Bathroom;
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
        SetCamera(currentRoom, destinationRoom);
    }

    private void SetCamera(Room destinationRoom, Room currentRoom)
    {

        switch (currentRoom)
        {
            case Room.Bathroom:
                    cameraToggle.SwitchCameras(CameraPosition.BedRoom1);
                break;
            case Room.BedRoom:

                if (destinationRoom == Room.LivingRoom)
                {
                    cameraToggle.SwitchCameras(CameraPosition.LivingRoom1);
                }
                else
                {
                    cameraToggle.SwitchCameras(CameraPosition.BathRoom);
                }
                break;
            case Room.LivingRoom:

                if (destinationRoom == Room.ToyRoom)
                {
                    cameraToggle.SwitchCameras(CameraPosition.ToyRoom1);
                } else
                {
                    cameraToggle.SwitchCameras(CameraPosition.BedRoom1);
                }
                break;
            case Room.ToyRoom:
                cameraToggle.SwitchCameras(CameraPosition.LivingRoom1);
                break;
            default:
                break;
        }




    }




}
