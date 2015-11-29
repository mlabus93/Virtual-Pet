using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace PersonalScripts
{
    public class UIController : MonoBehaviour
    {
        public Button attack1Btn;
        public Button attack2Btn;
        public Button jumpBtn;
        public Button statsBtn;
        public Button outfitBtn;
        public Button miniGameBtn;
        public Button cameraBtn;
        public Button optionsBtn;
        public Button pauseBtn;
        public Button resumeBtn;
        public Button exitBtn;
        public Button saveBtn;
        public Button securityCamBtn;
        public Button drinkBtn;
        public Button turkeyBtn;
        public Button treatBtn;
        public Button beefBtn;
        public Button ribBtn;
        public Button chickenBtn;
        public Button fishBtn;
        public Button helpBtn;
        public GameObject pauseParentPanel;
        public GameObject statsPanel;
        public GameObject optionsPanel;
        public GameObject helpPanel;
        public GameObject canvas;
        public Slider healthSlider;
        public Slider happinessSlider;
        public Slider fatigueSlider;
        public Slider hungerSlider;
        public Slider thirstSlider;
        public Slider bladderSlider;
        public GameObject player;
        public PlayerHealth playerHealth;
        public GameObject insufficientCoinsTxt;
        GameObject healthBar;
        Slider healthBarSlider;
        Slider[] sliders;
        IAnimalCharacter iAnimal;

        private void GetPanels()
        {
            statsPanel = GameObject.FindGameObjectWithTag("StatsPanel");
            pauseParentPanel = GameObject.Find("PauseParentPanel");
            optionsPanel = GameObject.Find("OptionsPanel");
            helpPanel = GameObject.Find("HelpPanel");
        }

        public void GetButtons()
        {
            // container for all buttons
            Button[] _buttons = gameObject.GetComponentsInChildren<Button>();
            for (int i = 0; i < _buttons.Length; i++)
            {
                string btnName = _buttons[i].name;

                if (btnName == "Attack1Btn")
                {
                    attack1Btn = _buttons[i];
                }
                if (btnName == "Attack2Btn")
                {
                    attack2Btn = _buttons[i];
                }
                if (btnName == "JumpBtn")
                {
                    jumpBtn = _buttons[i];
                }
                if (btnName == "Stats")
                {
                    statsBtn = _buttons[i];
                }
                if (btnName == "Outfit")
                {
                    outfitBtn = _buttons[i];
                }
                if (btnName == "PauseBtn")
                {
                   pauseBtn = _buttons[i];
                }
                if (btnName == "DrinkBtn")
                {
                    drinkBtn = _buttons[i];
                }
                if (btnName == "EatTurkeyBtn")
                {
                    turkeyBtn = _buttons[i];
                }
                if (btnName == "EatChickenBtn")
                {
                    chickenBtn = _buttons[i];
                }
                if (btnName == "EatRibsBtn")
                {
                    ribBtn = _buttons[i];
                }
                if (btnName == "EatTreatBtn")
                {
                    treatBtn = _buttons[i];
                }
                if (btnName == "EatBeefBtn")
                {
                    beefBtn = _buttons[i];
                }
                if (btnName == "EatFishBtn")
                {
                    fishBtn = _buttons[i];
                }
                if (btnName == "Help")
                {
                    helpBtn = _buttons[i];
                }
                /*
                if (btnName == "")
                {
                    temp = _buttons[i];
                }
                if (btnName == "")
                {
                    temp = _buttons[i];
                }
                if (btnName == "")
                {
                    temp = _buttons[i];
                }
                if (btnName == "")
                {
                    temp = _buttons[i];
                }
                if (btnName == "")
                {
                    temp = _buttons[i];
                }
                if (btnName == "")
                {
                    temp = _buttons[i];
                }
                */

            }
            // finds all buttons in scene and assigns them based on name
           //statsBtn = ;
           //outfitBtn = ;
           //miniGameBtn = ;
           //optionsBtn = ;
           //pauseBtn = ;
           //resumeBtn = ;
           //drinkBtn = ;
           //turkeyBtn = ;
           //treatBtn = ;
           //beefBtn = ;
           //ribBtn = ;
           //chickenBtn = ;
           //fishBtn = ;
           //pauseParentPanel = ;
           //statsPanel = ;
           //optionsPanel = ;
           //canvas = ;
           //healthSlider = ;
           //happinessSlider = ;
           //fatigueSlider = ;
           //hungerSlider = ;
           //thirstSlider = ;
           //bladderSlider = ;
           //player = ;
           //public PlayerHealth playerHealth = ;
           //insufficientCoinsTxt = ;
           //GameObject healthBar = ;
           //Slider healthBarSlider = ;
           //Slider[] sliders = ;
           //IAnimalCharacter iAnimal = ;


        }

        public void SetButtons(Button[] buttons)
        {
            foreach (var button in buttons)
            {
                string buttonName = button.name;
                switch (buttonName)
                {
                    case "PauseBtn":
                        pauseBtn = button;
                        break;
                    case "Stats":
                        statsBtn = button;
                        break;
                    case "Outfit":
                        outfitBtn = button;
                        break;
                    case "Minigame":
                        miniGameBtn = button;
                        break;
                    case "Options":
                        optionsBtn = button;
                        break;
                    case "Camera":
                        cameraBtn = button;
                        break;
                    case "ResumeBtn":
                        resumeBtn = button;
                        break;
                    case "ExitBtn":
                        exitBtn = button;
                        break;
                    case "SaveBtn":
                        saveBtn = button;
                        break;
                    case "SecurityCamBtn":
                        securityCamBtn = button;
                        break;
                    case "HelpBtn":
                        helpBtn = button;
                        break;
                    case "EatTurkeyBtn":
                        turkeyBtn = button;
                        break;
                    case "EatChickenBtn":
                        chickenBtn = button;
                        break;
                    case "EatRibsBtn":
                        ribBtn = button;
                        break;
                    case "EatTreatBtn":
                        treatBtn = button;
                        break;
                    case "EatBeefBtn":
                        beefBtn = button;
                        break;
                    case "EatFishBtn":
                        fishBtn = button;
                        break;
                    case "DrinkBtn":
                        drinkBtn  = button;
                        break;
                }
            }
        }

        public void SetPanels(GameObject[] panels)
        {
            if (panels != null)
            {
                foreach (var panel in panels)
                {
                    string panelName = panel.name;
                    switch (panelName)
                    {
                        case "PauseParentPanel":
                            pauseParentPanel = panel;
                            break;
                        case "StatsPanel":
                            statsPanel = panel;
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
        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                SetupController();
            }
            //GetButtons();
            //GetPanels();
            //healthBarSlider = healthBar.GetComponent<Slider>();
        }

        void SetupController()
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            iAnimal = player.GetComponent<IAnimalCharacter>();
            healthBarSlider = FindObjectOfType<Slider>();
        }
        
        public void SetUpFoodUI(GameObject noCoins)
        {
            //Need to set these options to disabled until the condition is 
            if(noCoins != null)
            {
                insufficientCoinsTxt = noCoins;
            }
            drinkBtn.gameObject.SetActive(false);
            turkeyBtn.gameObject.SetActive(false);
            chickenBtn.gameObject.SetActive(false);
            ribBtn.gameObject.SetActive(false);
            treatBtn.gameObject.SetActive(false);
            beefBtn.gameObject.SetActive(false);
            fishBtn.gameObject.SetActive(false);
            insufficientCoinsTxt.SetActive(false);
        }
        //private void UpdateHealthBar()
        //{
        //    if (healthBarSlider != null)
        //    {
        //        GameObject healthValue = GameObject.Find("HealthValue");
        //        Text value = healthValue.GetComponent<Text>();
        //        healthBarSlider.value = playerHealth.currentHealth;
        //        if (playerHealth.currentHealth >= 0)
        //        {
        //            value.text = playerHealth.currentHealth.ToString();
        //        }
        //        else
        //        {
        //            value.text = "0";
        //        }
        //    }
        //}

        public void ShowPausePanel()
        {
            if (pauseBtn.gameObject.activeSelf)
            {
                pauseBtn.gameObject.SetActive(false);
                pauseParentPanel.gameObject.SetActive(true);
                resumeBtn.gameObject.SetActive(true);
            }
        }

        public void ShowHelpPanel()
        {
            if (helpBtn.gameObject.activeSelf)
            {
                helpPanel.gameObject.SetActive(true);
            }
        }
        public void CloseHelpPanel()
        {
            helpPanel.SetActive(false);
        }

        public void HidePausePanel()
        {
            if (pauseParentPanel.gameObject.activeSelf)
            {
                pauseParentPanel.gameObject.SetActive(false);
                pauseBtn.gameObject.SetActive(true);
            }
        }

        public void ShowStats()
        {
            //statsBtn = stats;
            //this.statsPanel = statsPanel;
            if (statsBtn.gameObject.activeSelf)
            {
                HideStatsBtn();
                ShowStatsPanel();
            }
        }

        public void ShowOptions()
        {
            if (optionsBtn.gameObject.activeSelf)
            {
                HideOptionsBtn();
                ShowOptionsPanel();
            }
        }

        public void ChangeOutfit()
        {
            OutfitChange script = player.GetComponentInChildren<OutfitChange>();//GetComponent<OutfitChange>();
            script.ChangeOutfit(0, true);
        }

        public void CloseOptionsPanel()
        {
            HideOptionsPanel();
            ShowOptionsBtn();
        }

        private void ShowStatsPanel()
        {
            if (statsPanel != null)
            {
                statsPanel.gameObject.SetActive(true);
            }
        }

        private void HideStatsPanel()
        {
            statsPanel.gameObject.SetActive(false);
        }

        private void ShowStatsBtn()
        {
            statsBtn.gameObject.SetActive(true);
        }

        private void HideStatsBtn()
        {
            statsBtn.gameObject.SetActive(false);
        }

        private void ShowOptionsPanel()
        {
            optionsPanel.gameObject.SetActive(true);
        }

        private void HideOptionsPanel()
        {
            if (optionsPanel != null)
            {
                optionsPanel.gameObject.SetActive(false);
            }
        }

        private void ShowOptionsBtn()
        {
            optionsBtn.gameObject.SetActive(true);
        }

        private void HideOptionsBtn()
        {
            optionsBtn.gameObject.SetActive(false);
        }

        private void HidePanels()
        {
            HideStatsPanel();
            HideOptionsPanel();
        }

        private void ShowBtns()
        {
            ShowStatsBtn();
            ShowOptionsBtn();
        }

        private bool CheckForActivePanels()
        {
            if (statsPanel.gameObject.activeSelf || optionsPanel.gameObject.activeSelf)
            {
                return true;
            }
            return false;
        }

        // Update is called once per frame
        void Update()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            //UpdateHealthBar();
            if (statsPanel)
            {
                if (Input.GetButtonDown("Fire1") && statsPanel.gameObject.activeSelf)
                {
                    HidePanels();
                    ShowBtns();
                }
            }
        }
    }

}
