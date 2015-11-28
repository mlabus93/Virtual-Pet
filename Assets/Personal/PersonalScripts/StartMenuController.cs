using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenuController : MonoBehaviour {

    public GameObject settingsPanel;
    public Button continueBtn;
    public Button newGameBtn;
    public Button settingsBtn;
    public Button closeOptionsBtn;

	// Use this for initialization
	void Start () {
        ToggleContinueButton();
	}

    public void ToggleContinueButton()
    {
        PersonalScripts.AnimalGameManager agm = GameObject.Find("GameManager").GetComponent<PersonalScripts.AnimalGameManager>();
        
        if (!agm.Load())
        {
            continueBtn.gameObject.SetActive(true);
        }
    }

    public void ShowSettings()
    {
        settingsPanel.gameObject.SetActive(true);
    }

    public void HideSettings()
    {
        settingsPanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
