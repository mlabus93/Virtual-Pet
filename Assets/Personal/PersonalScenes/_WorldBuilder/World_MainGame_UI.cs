// Project: Pet Pals
// File: World_MainGame_UI.cs
// Modification History:
// Author           Date
// Labus			11/25/15
// Jean-Baptiste 	11/26/15
// Labus			11/27/15
// Jean-Baptiste    11/27/15
// Jean-Baptiste    11/28/15
// Jean-Baptiste	11/29/15
// Mirvil			11/29/15
// Labus			11/29/15

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
    Button hauntedGameBtn;
    Button jumpGameBtn;
    Button closeMiniGamePanelBtn;
    GameObject miniGamePanel;
    Text coinText;
    Slider[] _sliders;
    Slider volumeSlider;

    public override void SetupButtons()
    {
        SetPlayer();
        if (_buttons == null)
        {
            _buttons = GameObject.FindObjectsOfType<Button>();
        }
        if (_sliders == null)
        {
            SetupSliders();
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
        SetCoinTextLabel();
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
                    miniGameBtn = _buttons[i];
                    _buttons[i].onClick.AddListener(() =>
                    {
                        miniGameBtn.gameObject.SetActive(false);
                        miniGamePanel.gameObject.SetActive(true);
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
                case "HauntedGameBtn":
                    hauntedGameBtn = _buttons[i];
                    _buttons[i].onClick.AddListener(() =>
                    {
                        PersonalScripts.AnimalGameManager man = FindObjectOfType<PersonalScripts.AnimalGameManager>();
                        Destroy(man.PlayerAnimalObject);
                        man.PlayerAnimalObject = null;
                        uiManager.GetComponent<SceneSwapper>().LoadScene("HauntedMiniGame");
                    });
                    break;
                case "JumpGameBtn":
                    jumpGameBtn = _buttons[i];
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiManager.GetComponent<SceneSwapper>().LoadScene("Jumpgame");
                    });
                    break;
                case "CloseMiniGameBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        miniGamePanel.gameObject.SetActive(false);
                        miniGameBtn.gameObject.SetActive(true);
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
                case "MiniGamePanel":
                    miniGamePanel = panel;
                    break;
            }
        }
    }

    public void SetupSliders()
    {
        _sliders = GameObject.FindObjectsOfType<Slider>();
        foreach (var slider in _sliders)
        {
            string sliderName = slider.name;
            switch (sliderName)
            {
                case "VolumeSlider":
                    volumeSlider = slider;
                    slider.onValueChanged.AddListener(delegate
                    {
                        AudioSource music = GameObject.Find("Overhead").GetComponentInChildren<AudioSource>();
                        music.volume = volumeSlider.value;
                    });
                    break;
            }
        }
    }

    void SetCoinTextLabel()
    {  
        coinText = GameObject.Find("CoinsText").GetComponent<Text>();
    }

    void Update()
    {
        coinText.text = "$" + PersonalScripts.AnimalGameManager._coins;
    }
}
