// Project: Pet Pals
// File: PickUpWeapon.cs
// Modification History:
// Author           Date
// Jean-Baptiste    11/23/15
// Jean-Baptiste    11/28/15

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using PersonalScripts;
public class PickUpWeapon : MonoBehaviour {
    public int _weapon = 0;
    public AudioSource _audioSource;
    public float destroyIn = 10;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        destroyIn -= Time.deltaTime;
        // deletes object from scene
        if (destroyIn <= 0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _audioSource.Play();
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
