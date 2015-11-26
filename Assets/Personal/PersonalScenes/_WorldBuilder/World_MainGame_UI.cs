using UnityEngine;
using System.Collections;
using PersonalScripts;
using UnityEngine.UI;
public class World_MainGame_UI : DynamicButtonAssignment
{

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
	
	}

    public override void SetupButtons()
    {
        SetPlayer();
        if (_buttons == null)
        {
            _buttons = GameObject.FindObjectsOfType<Button>();
        }

        GameObject uiManager = GameObject.FindGameObjectWithTag("UIManager");
        // Checks names of buttons and adds listeners accordingly
        for (int i = 0; i < _buttons.Length; i++)
        {
            string btnName = _buttons[i].name;

            switch (btnName)
            {
                case "BackBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiManager.GetComponent<SceneSwapper>().LoadScene(0);
                    });
                    break;
                case "StartBtn":
                    _buttons[i].onClick.AddListener(() =>
                    {
                        uiManager.GetComponent<World_CharacterSelect>().CharacterSelected();
                        uiManager.GetComponent<SceneSwapper>().LoadScene(2);

                    });
                    break;
                case "AnimalLeftBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().PrevCharacter();
                    });
                    break;
                case "AnimalRightBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().NextCharacter();
                    });
                    break;
                case "ClothingLeftBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().PrevOutfit();
                    });
                    break;
                case "ClothingRightBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().NextOutfit();
                    });
                    break;
                case "EyesLeftBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().EyesToggle();
                    });
                    break;
                case "EyesRightBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().EyesToggle();
                    });
                    break;
                case "HatLeftBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().PrevHat();
                    });
                    break;
                case "HatRightBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().NextHat();
                    });
                    break;
                case "WeaponLeftBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().PrevWeapon();
                    });
                    break;
                case "WeaponRightBtn":
                    _buttons[i].onClick.AddListener(delegate
                    {
                        uiManager.GetComponent<CharacterSelection>().NextWeapon();
                    });
                    break;
            }
        }
    }
}
