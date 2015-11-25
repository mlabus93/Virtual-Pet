using UnityEngine;
using System.Collections;

public class UISpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpawnUI();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnUI()
    {
        Canvas ui = Instantiate(Resources.Load("UI/CanvasCharacterSelect", typeof(Canvas)), Vector3.zero, Quaternion.identity) as Canvas;
    }
}
