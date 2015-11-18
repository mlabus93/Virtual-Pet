using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour {

    public Button statsBtn;
    public Button outfitBtn;
    public Button miniGameBtn;
    public Button optionsBtn;
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
    GameObject healthBar;
    Slider healthBarSlider;
    IAnimalCharacter iAnimal;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        iAnimal = player.GetComponent<IAnimalCharacter>();
        //healthBar = GameObject.FindGameObjectWithTag("HealthBar");
        //healthBarSlider = healthBar.GetComponent<Slider>();
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
	void Update ()
    {
        if (Input.GetButtonDown("Fire1") && statsPanel.gameObject.activeSelf)
        {
            HidePanels();
            ShowBtns();
        }
    }
}
