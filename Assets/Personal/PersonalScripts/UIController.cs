using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace PersonalScripts
{
    public class UIController : MonoBehaviour
    {

        public Button statsBtn;
        public Button outfitBtn;
        public Button miniGameBtn;
        public Button optionsBtn;
        public Button pauseBtn;
        public Button resumeBtn;
        public Button drinkBtn;
        public Button turkeyBtn;
        public Button treatBtn;
        public Button beefBtn;
        public Button ribBtn;
        public Button chickenBtn;
        public Button fishBtn;
        public GameObject pauseParentPanel;
        public GameObject statsPanel;
        public GameObject optionsPanel;
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

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            iAnimal = player.GetComponent<IAnimalCharacter>();
            healthBarSlider = FindObjectOfType<Slider>();
            //healthBarSlider = healthBar.GetComponent<Slider>();
        }

        private void UpdateHealthBar()
        {
            if (healthBarSlider != null)
            {
                GameObject healthValue = GameObject.Find("HealthValue");
                Text value = healthValue.GetComponent<Text>();
                healthBarSlider.value = playerHealth.currentHealth;
                if (playerHealth.currentHealth >= 0)
                {
                    value.text = playerHealth.currentHealth.ToString();
                }
                else
                {
                    value.text = "0";
                }
            }
        }

        public void ShowPausePanel()
        {
            if (pauseBtn.gameObject.activeSelf)
            {
                pauseBtn.gameObject.SetActive(false);
                pauseParentPanel.gameObject.SetActive(true);
                resumeBtn.gameObject.SetActive(true);
            }
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
            statsPanel.gameObject.SetActive(true);
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
            optionsPanel.gameObject.SetActive(false);
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
            UpdateHealthBar();
            if (Input.GetButtonDown("Fire1") && statsPanel.gameObject.activeSelf)
            {
                HidePanels();
                ShowBtns();
            }
        }
    }

}
