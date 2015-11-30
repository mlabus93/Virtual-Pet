// Project: Pet Pals
// File: UISpawner.cs
// Modification History:
// Author           Date
// Labus            11/24/15
// Jean-Baptiste    11/26/15
// Jean-Baptiste    11/27/15
// Labus            11/27/15
// Labus            11/29/15

using UnityEngine;
using System.Collections;

public class UISpawner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        if (Application.loadedLevelName == "Jumpgame")
        {
            SpawnUI();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SpawnUI()
    {
        if (Application.loadedLevelName == "CharacterSelect")
        {
            Canvas ui = Instantiate(Resources.Load("UI/CanvasCharacterSelect", typeof(Canvas)), Vector3.zero, Quaternion.identity) as Canvas;
        }
        if (Application.loadedLevelName == "Main")
        {
            Canvas ui = Instantiate(Resources.Load("UI/CanvasMain", typeof(Canvas)), Vector3.zero, Quaternion.identity) as Canvas;
            //GameObject statsPanel = Instantiate(Resources.Load("UI/StatsPanel", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            //GameObject optionsPanel = Instantiate(Resources.Load("UI/OptionsPanel", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
            //GameObject pauseParentPanel = Instantiate(Resources.Load("UI/PauseParentPanel", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
        }
        if (Application.loadedLevelName == "HauntedMiniGame")
        {
            Canvas ui = Instantiate(Resources.Load("UI/CanvasMiniGame", typeof(Canvas)), Vector3.zero, Quaternion.identity) as Canvas;
        }
        if (Application.loadedLevelName == "Jumpgame")
        {
            Canvas ui = Instantiate(Resources.Load("UI/CanvasMiniGame2", typeof(Canvas)), Vector3.zero, Quaternion.identity) as Canvas;
        }
    }
}
