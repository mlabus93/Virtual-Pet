using UnityEngine;
using System.Collections;
using PersonalScripts;
using UnityEngine.UI;
public class World_MainGame_UI : DynamicButtonAssignment
{
    Button attack1Btn;
    Button attack2Btn;
    Button jumpBtn;
    Button statsBtn;
    Button outfitBtn;
    Button miniGameBtn;
    Button optionsBtn;
    Button pauseBtn;
    Button resumeBtn;
    Button drinkBtn;
    Button turkeyBtn;
    Button treatBtn;
    Button beefBtn;
    Button ribBtn;
    Button chickenBtn;
    Button fishBtn;

    public override void SetupButtons()
    {
        SetPlayer();
        if (_buttons == null)
        {
            _buttons = GameObject.FindObjectsOfType<Button>();
        }
        if (_panels == null)
        {
            SetupPanels();
        }
        GameObject uiManager = GameObject.FindGameObjectWithTag("UIManager");
        Canvas canvasMain = GameObject.FindObjectOfType<Canvas>();
        UIController uiController = canvasMain.GetComponent<UIController>();
        uiController.SetButtons(_buttons);
        uiController.SetPanels(_panels);
        // Checks names of buttons and adds listeners accordingly
        for (int i = 0; i < _buttons.Length; i++)
        {
            string btnName = _buttons[i].name;

            switch (btnName)
            {
                case "Stats":
                    statsBtn = _buttons[i];
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiController.ShowStats();
                    });
                    break;
                case "PauseBtn":
                    pauseBtn = _buttons[i];
                    _buttons[i].onClick.AddListener(() =>
                    {
                        Time.timeScale = 0;
                        uiController.ShowPausePanel();
                    });
                    break;
                case "Outfit":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiController.ChangeOutfit();
                    });
                    break;
                case "Options":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiController.ShowOptions();
                    });
                    break;
                case "CloseOptionsBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiController.CloseOptionsPanel();
                    });
                    break;
                case "Minigame":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiManager.GetComponent<SceneSwapper>().LoadScene(4);
                    });
                    break;
                case "ResumeBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        Time.timeScale = 1;
                        uiController.HidePausePanel();
                    });
                    break;
                case "Camera":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("Cameras").GetComponent<ToggleCameras>().ChangeCameraRoom();
                    });
                    break;
                case "ExitBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        
                        uiManager.GetComponent<LevelManager>().ReturnToStartMenu();
                    });
                    break;
                case "TopCamBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("Cameras").GetComponent<ToggleCameras>().ToggleOverheadCameraView();
                    });
                    break;
                case "SecurityCamBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("Cameras").GetComponent<ToggleCameras>().ToggleSecurityCameraView();
                    });
                    break;
                case "SaveBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {

                    });
                    break;
                case "HelpBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiController.ShowHelpPanel();
                    });
                    break;
                case "CloseHelpBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiController.CloseHelpPanel();
                    });
                    break;
                case "DrinkBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("FoodManager").GetComponent<ManageFoods>().Drink();
                    });
                    break;
                case "EatTurkeyBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("FoodManager").GetComponent<ManageFoods>().EatTurkey();
                    });
                    break;
                case "EatChickenBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("FoodManager").GetComponent<ManageFoods>().EatChicken();
                    });
                    break;
                case "EatRibsBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("FoodManager").GetComponent<ManageFoods>().EatRibs();
                    });
                    break;
                case "EatTreatBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("FoodManager").GetComponent<ManageFoods>().EatTreat();
                    });
                    break;
                case "EatBeefBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("FoodManager").GetComponent<ManageFoods>().EatBeef();
                    });
                    break;
                case "EatFishBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        GameObject.Find("FoodManager").GetComponent<ManageFoods>().EatFish();
                    });
                    break;
            }
        }
        uiController.SetUpFoodUI(GameObject.FindGameObjectWithTag("NoCoins"));
    }

    public void SetupPanels()
    {
        if (_panels == null)
        {
            _panels = GameObject.FindGameObjectsWithTag("Panel");
        }
        foreach (var panel in _panels)
        {
            string panelName = panel.name;
            panel.gameObject.SetActive(false);
            switch (panelName)
            {
                case "StatsPanel":
                    statsPanel = panel;
                    break;
                case "PauseParentPanel":
                    pauseParentPanel = panel;
                    break;
                case "OptionsPanel":
                    optionsPanel = panel;
                    break;
                case "HelpPanel":
                    helpPanel = panel;
                    break;
            }
        }
    }
}
