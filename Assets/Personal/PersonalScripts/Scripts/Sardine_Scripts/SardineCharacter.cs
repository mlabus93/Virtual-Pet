using UnityEngine;
using System.Collections;

public class SardineCharacter : MonoBehaviour {
	public Animator sardineAnimator;
	Rigidbody sardineRigid;
	public float turnSpeed=5f;
	public float forwardSpeed=5f;

	void Start () {
		sardineAnimator = GetComponent<Animator> ();
		sardineRigid = GetComponent<Rigidbody> ();
	}

	public void TurnLeft(){
		sardineRigid.AddTorque (-transform.up*turnSpeed,ForceMode.Impulse);
		sardineAnimator.SetTrigger ("TurnLeft");
	}

	public void TurnRight(){
		sardineRigid.AddTorque (transform.up*turnSpeed,ForceMode.Impulse);
		sardineAnimator.SetTrigger ("TurnRight");
	}

	public void MoveForward(){
		sardineRigid.AddForce (transform.forward*forwardSpeed,ForceMode.Impulse);
		sardineAnimator.SetTrigger ("MoveForward");
	}



	public void TurnUp(){
		sardineRigid.AddTorque (-transform.right*turnSpeed,ForceMode.Impulse);
	}

	public void TurnDown(){
		sardineRigid.AddTorque (transform.right*turnSpeed,ForceMode.Impulse);
	}


	public void Move(float v,float h){
		sardineAnimator.SetFloat ("Forward", v);
		sardineAnimator.SetFloat ("Turn", h);
		sardineRigid.AddForce (transform.forward*forwardSpeed*v);
		sardineRigid.AddTorque (transform.up*turnSpeed*h);
	}



}
