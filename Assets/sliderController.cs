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

    _player = _PlayerObj.GetComponent<IAnimalCharacter>();


    if (_player != null)
        Debug.Log("It is not NULL");
    else
    {
        Debug.Log("it is null");
        Debug.Log(_PlayerObj.name);
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
                _statusSliders[i].value = 5;
            }
        }
	}
}
