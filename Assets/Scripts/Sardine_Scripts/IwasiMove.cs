using UnityEngine;
using System.Collections;

public class IwasiMove : MonoBehaviour {
	public float speed=1.5f;
	public float rotateSpeed=15f;
	public Transform target;  
	Vector3 targetRelPos;

	void Update () {
		targetRelPos = target.position - transform.position;

		Rigidbody iwasirigid = GetComponent<Rigidbody> ();

			float dottigawa = Vector3.Dot (targetRelPos, transform.right);
			if (dottigawa < 0) {
				iwasirigid.AddTorque(-Vector3.up * Time.deltaTime * rotateSpeed);
			} else if (dottigawa > 0) {
				iwasirigid.AddTorque(Vector3.up * Time.deltaTime * rotateSpeed);
			}
		iwasirigid.velocity= transform.forward * speed;
	}
}
