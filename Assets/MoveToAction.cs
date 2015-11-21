using UnityEngine;
using System.Collections;

public class MoveToAction : MonoBehaviour {
    public GameObject eatPosition;
    public GameObject drinkPosition;
    public GameObject[] randomPositions;
    public bool inTarget = false;
    //public bool isEating = false;
    public bool isDrinking = false;


    Animator anim;
    Transform table;
    Transform bed;
    Transform toyDoll;
    Transform toyBall;
    Transform toilet;
    Transform currentRandomTarget;
    NavMeshAgent nav;
    string currentTarget = "";
    bool moveRandom = true;
    bool randomTargetFound = true;



    // Use this for initialization
    void Awake () {
        table = GameObject.FindGameObjectWithTag("Food Table").transform;
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //bed = GameObject.FindGameObjectWithTag("Bed").transform;
        //toyDoll = GameObject.FindGameObjectWithTag("Toy Doll").transform;
        //toyBall = GameObject.FindGameObjectWithTag("Toy Ball").transform;
        //toilet = GameObject.FindGameObjectWithTag("Toilet").transform;
        nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {

        if (!moveRandom)
        {
            if (currentTarget.Equals("table") && !inTarget)
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
            else if (currentTarget.Equals("bowl") && !inTarget)
            {
                nav.SetDestination(drinkPosition.transform.position);
            }
        }else
        {
            MoveRandomly();
        }
    }

    public void MoveRandomly()
    {
        if(randomTargetFound)
        {
            var oldTarget = currentRandomTarget;
            int newSpot = Random.Range(0, randomPositions.Length);
            currentRandomTarget = randomPositions[newSpot].transform;
            currentRandomTarget = oldTarget == null || oldTarget != currentRandomTarget ? currentRandomTarget : randomPositions[(newSpot + 1) % randomPositions.Length].transform;
            randomTargetFound = false;
        }
        nav.SetDestination(currentRandomTarget.position);
        anim.SetBool("Attack1", true);
        //anim.SetFloat()
        //anim.SetFloat("vely", velocity.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == currentRandomTarget)
        {
            randomTargetFound = true;
        }
    }

    public void StopPlayer()
    {
        nav.Stop();
    }

    public void GoToWaterBowl()
    {
        currentTarget = "bowl";
        
    }

    public void GoToFoodPlate()
    {
        nav.SetDestination(eatPosition.transform.position);
        //plaa
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
