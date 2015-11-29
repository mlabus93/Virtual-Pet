using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using PersonalScripts;
public class PickUpWeapon : MonoBehaviour {
    public int _weapon = 0;
    public AudioSource _audioSource;
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
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
