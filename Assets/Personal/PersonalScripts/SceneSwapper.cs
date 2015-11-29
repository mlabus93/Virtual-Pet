// Project: Pet Pals
// File: PlayerSpawner.cs
// Modification History:
// Author           Date
// Jean-Baptiste    11/23/15
// Labus            11/24/15
// Jean-Baptiste    11/28/15

using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class SceneSwapper : MonoBehaviour
    {

        public void LoadScene(int sceneNum)
        {
            // save game before loading scene
            //GetComponent<AnimalGameManager>().Save();
            AnimalGameManager agm = FindObjectOfType<AnimalGameManager>();
            if (agm != null)
            {
                agm.Save();
            }
            // SceneNum relates to the index in 
            // File -> BuildSettings -> Scenes In Build
            Application.LoadLevel(sceneNum);
        }

        public void LoadScene(string sceneName)
        {
            // save game before loading scene
            //GetComponent<AnimalGameManager>().Save();
            AnimalGameManager agm = FindObjectOfType<AnimalGameManager>();
            if (agm != null)
            {
                agm.Save();
            }
            // SceneNum relates to the index in 
            // File -> BuildSettings -> Scenes In Build
            Application.LoadLevel(sceneName);
        }
    }

}
