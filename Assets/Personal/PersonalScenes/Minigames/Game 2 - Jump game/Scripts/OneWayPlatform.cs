using UnityEngine;
using System.Collections;

public class OneWayPlatform : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.parent.GetComponent<Collider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Foot Collider")
            transform.parent.GetComponent<Collider>().isTrigger = false;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Foot Collider")
            transform.parent.GetComponent<Collider>().isTrigger = true;
    }
}