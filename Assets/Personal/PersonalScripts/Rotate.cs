// Project: Pet Pals
// File: Rotate.cs
// Modification History:
// Author           Date
// Jean-Baptiste    11/23/15

using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public float speed = 10f;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
	}
}
