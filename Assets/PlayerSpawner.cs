using UnityEngine;
using System.Collections;
////////////////////////////////////////////////////////////
///<summary>
/// Description: This script will find which species animal is saved
/// in the game manager and instantiate that breed, it will 
/// then load the last sdaved animal configurations to that animal
/// breed
/// </summary> 
///////////////////////////////////////////////////////////
 
public class PlayerSpawner : MonoBehaviour {
    // Playable animal characterssdfasd
    GameObject[] _characters;
    GameManager _manager;
 
	// Use this for initialization
	void Start () 
    {
	    if (_characters.Length < 1)
        {
            Debug.LogError("NO PLAYABLE ANIMAL AVAILABLE");
        }
	}

	// Update is called once per frame
	void Update () {
	
	}
}
