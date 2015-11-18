using UnityEngine;
using System.Collections;

public class SceneSwapper : MonoBehaviour {


    //TODO: make enums for all of the levels
    public void LoadScene(int sceneNum)
    {
        // save game before loading scene
        GetComponent<GameManager>().Save();
        // SceneNum relates to the index in 
        // File -> BuildSettings -> Scenes In Build
        Application.LoadLevel(sceneNum);
    }
}
