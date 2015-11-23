using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public float maxVelocity = 20;
    public float minVelocity = 15;

	void Awake () {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -18);
	}

	void Update () {
        //Make sure we stay between the MAX and MIN speed.
        float totalVelocity = Vector3.Magnitude(GetComponent<Rigidbody>().velocity);
        if(totalVelocity>maxVelocity){
            float tooHard = totalVelocity / maxVelocity;
            GetComponent<Rigidbody>().velocity /= tooHard;
        }
        else if (totalVelocity < minVelocity)
        {
            float tooSlowRate = totalVelocity / minVelocity;
            GetComponent<Rigidbody>().velocity /= tooSlowRate;
        }

        //Is the ball below -3? Then we're game over.
        if(transform.position.z <= -3){            
            BreakoutGame.SP.LostBall();
            Destroy(gameObject);
        }
	}
}
