using UnityEngine;
using System.Collections;

public class MoveToAction : MonoBehaviour {
    public Collider tableCol;
    Transform table;
    Transform bed;
    Transform toyDoll;
    Transform toyBall;
    Transform toilet;
    NavMeshAgent nav;
    string currentTarget = "";
    bool inTarget = false;
    // Use this for initialization
    void Awake () {
        table = GameObject.FindGameObjectWithTag("Food Table").transform;
        //bed = GameObject.FindGameObjectWithTag("Bed").transform;
        //toyDoll = GameObject.FindGameObjectWithTag("Toy Doll").transform;
        //toyBall = GameObject.FindGameObjectWithTag("Toy Ball").transform;
        //toilet = GameObject.FindGameObjectWithTag("Toilet").transform;
        nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

        if(currentTarget.Equals("table") && !inTarget)
        {
            nav.SetDestination(table.position);
        }
        //else if(currentTarget.Equals("bed") && !inTarget)
        //{
        //    nav.SetDestination(bed.position);
        //}
        //else if (currentTarget.Equals("doll") && !inTarget)
        //{
        //    nav.SetDestination(toyDoll.position);
        //}
        //else if (currentTarget.Equals("ball") && !inTarget)
        //{
        //    nav.SetDestination(toyBall.position);
        //}
        //else if (currentTarget.Equals("toilet") && !inTarget)
        //{
        //    nav.SetDestination(toilet.position);
        //}
    }

    public void GoToFoodTable()
    {
        inTarget = false;
        currentTarget = "table";
    }

    public void GoToBed()
    {
        inTarget = false;
        currentTarget = "bed";
    }

    public void PlayWithDoll()
    {
        inTarget = false;
        currentTarget = "doll";
    }

    public void PlayWithBall()
    {
        inTarget = false;
        currentTarget = "ball";
    }

    public void UseRestRoom()
    {
        inTarget = false;
        currentTarget = "toilet";
    }
}
