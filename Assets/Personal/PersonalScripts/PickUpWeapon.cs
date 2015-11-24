using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using PersonalScripts;
public class PickUpWeapon : MonoBehaviour {
    public int _weapon = 0;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<WeaponHandler>().ChangeWeapons(_weapon, false);
            Destroy(gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

        }
    }
}
