using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIController : MonoBehaviour {

    public Button statsBtn;
    public Button outfitBtn;
    public Button miniGameBtn;
    public Button settingsBtn;
    public GameObject statsPanel;
    public GameObject canvas;
    public Slider healthSlider;
    public Slider happinessSlider;
    public Slider fatigueSlider;
    public Slider hungerSlider;
    public Slider thirstSlider;
    public Slider bladderSlider;

	// Use this for initialization
	void Start ()
    {

	}

    public void ShowStats()
    {
        if (statsBtn.gameObject.activeSelf)
        {
            HideStatsBtn();
            ShowStatsPanel();
        }
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
	// Update is called once per frame
	void Update ()
    {
        
        if (Input.GetButtonDown("Fire1") && !(statsBtn.gameObject.activeSelf))
        {
            HideStatsPanel();
            ShowStatsBtn();
        }
    }
}
