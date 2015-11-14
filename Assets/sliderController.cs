using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class sliderController : MonoBehaviour {
    Slider[] _statusSliders;
    public GameObject _PlayerObj;
    private IAnimalCharacter _player;

	// Use this for initialization
	void Start () {
	_statusSliders = GetComponentsInChildren<Slider>();
    _PlayerObj = GameObject.FindGameObjectWithTag("Player");

    _player = GameObject.FindObjectOfType<CatCharacter>();


    if (_player != null)
        Debug.Log("It is not NULL");
    else
    {
        Debug.Log("it is null");
        Debug.Log(_PlayerObj.name);
    }
	}
	
    public void ChangeSliderRightText(Text[] txts, int val)
    {
        for (int j = 0; j < txts.Length; j++)
        {
            string lblName = txts[j].name;
            if (lblName == "value")
            {
                txts[j].text = val.ToString();
            }
        }
    }
	// Update is called once per frame
	void Update () 
    {
        //Debug.Log(_statusSliders.Length);
	    for (int i = 0; i < _statusSliders.Length; i++)
        {
            string name = _statusSliders[i].name;
            
            if (name == "HealthSlider")
            {
                // changes slider's length
                _statusSliders[i].value = _player.health;

                // changes text next to slider to appropriate value
                Text[] txts =_statusSliders[i].GetComponentsInChildren<Text>();
                ChangeSliderRightText(txts, _player.health);
            }
            if (name == "HappinessSlider")
            {
                _statusSliders[i].value = _player.happiness;

                // changes text next to slider to appropriate value
                Text[] txts = _statusSliders[i].GetComponentsInChildren<Text>();
                ChangeSliderRightText(txts, _player.happiness);
            }
            if (name == "FatigueSlider")
            {
                _statusSliders[i].value = _player.fatigue;

                // changes text next to slider to appropriate value
                Text[] txts = _statusSliders[i].GetComponentsInChildren<Text>();
                ChangeSliderRightText(txts, _player.fatigue);
            }
            if (name == "HungerSlider")
            {
                _statusSliders[i].value = _player.hunger;

                // changes text next to slider to appropriate value
                Text[] txts = _statusSliders[i].GetComponentsInChildren<Text>();
                ChangeSliderRightText(txts, _player.hunger);
            }
            if (name == "ThirstSlider")
            {
                _statusSliders[i].value = _player.thirst;

                // changes text next to slider to appropriate value
                Text[] txts = _statusSliders[i].GetComponentsInChildren<Text>();
                ChangeSliderRightText(txts, _player.thirst);
            }
            if (name == "BladderSlider")
            {
                _statusSliders[i].value = _player.bladderCapacity;

                // changes text next to slider to appropriate value
                Text[] txts = _statusSliders[i].GetComponentsInChildren<Text>();
                ChangeSliderRightText(txts, _player.bladderCapacity);
            }
            if (name == "BoredomSlider")
            {
                _statusSliders[i].value = _player.boredom;

                // changes text next to slider to appropriate value
                Text[] txts = _statusSliders[i].GetComponentsInChildren<Text>();
                ChangeSliderRightText(txts, _player.boredom);
            }
        }
	}
}
