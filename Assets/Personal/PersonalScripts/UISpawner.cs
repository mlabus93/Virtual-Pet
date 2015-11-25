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
        if (Application.loadedLevelName == "CharacterSelect")
        {
            Canvas ui = Instantiate(Resources.Load("UI/CanvasCharacterSelect", typeof(Canvas)), Vector3.zero, Quaternion.identity) as Canvas;
        }
        if (Application.loadedLevelName == "Main")
        {
            Canvas ui = Instantiate(Resources.Load("UI/CanvasMain", typeof(Canvas)), Vector3.zero, Quaternion.identity) as Canvas;
        }
    }
}
