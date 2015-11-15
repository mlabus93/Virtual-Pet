using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchMovementController : MonoBehaviour {

    Text txt;
    Camera cam;
    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    Rigidbody _rigidBody;

	// Use this for initialization
	void Awake () {
        txt = GetComponent<Text>();
        floorMask = LayerMask.GetMask("Floor");
        _rigidBody = GameObject.FindObjectOfType<CatCharacter>().GetComponent<Rigidbody>();
	}

    void OnGUI()
    {
        Event e = Event.current;

        txt.text = "x: " + e.mousePosition.x + " y: " + e.mousePosition.y;
       


        // Create a ray from the mouse cursor on screen in the direction of the camera.
        //Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray camRay = Camera.main.ScreenPointToRay(e.mousePosition);
        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;
            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotatation = Quaternion.LookRotation(playerToMouse);
 
            _rigidBody.MoveRotation(newRotatation);

            Vector3 newLoci = new Vector3(floorHit.point.x, 0, floorHit.point.z);
            _rigidBody.MovePosition(newLoci);
        }

    }

    void FixedUpdate ()
    {

    }

}
