using UnityEngine;
using System.Collections;
using PersonalScripts;
using UnityEngine.UI;


class World_MiniGame_01_UI : DynamicButtonAssignment
{
    Button optionsBtn;
    int outfitIndex = 0;

    public override void SetupButtons()
    {

        if (_buttons == null)
        {
            _buttons = GameObject.FindObjectsOfType<Button>();
        }

        // Checks names of buttons and adds listeners accordingly
        for (int i = 0; i < _buttons.Length; i++)
        {
            string btnName = _buttons[i].name;

            if (btnName == "JumpBtn")
            {
                _buttons[i].onClick.AddListener(delegate
                {
                    GameObject.FindObjectOfType<Player1StickMovement>().GetComponent<Player1StickMovement>().Jump(3f);
                });
            }
            if (btnName == "Attack1Btn")
            {
                _buttons[i].onClick.AddListener(delegate
                {
                    _player.Attack(1);

                });
            }
            if (btnName == "Attack2Btn")
            {
                _buttons[i].onClick.AddListener(delegate
                {
                    _player.Attack(2);
                });
            }
            if (btnName == "Outfit")
            {
                _buttons[i].onClick.AddListener(delegate
                {
                    outfitIndex++;
                    if (outfitIndex > 3)
                    {
                        outfitIndex = 0;
                    }
                    _player.ChangeIntoSpecificFit(outfitIndex);
                });
            }
            if (btnName == "Options")
            {
                optionsBtn = _buttons[i];
                _buttons[i].onClick.AddListener(delegate
                {
                    Time.timeScale = 0;
                    optionsBtn.gameObject.SetActive(false);
                    optionsPanel.gameObject.SetActive(true);
                });
            }
            if (btnName == "CloseOptionsBtn")
            {
                _buttons[i].onClick.AddListener(delegate
                {
                    Time.timeScale = 1;
                    optionsPanel.gameObject.SetActive(false);
                    optionsBtn.gameObject.SetActive(true);
                });
            }
        }
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
                case "OptionsPanel":
                    optionsPanel = panel;
                    break;
            }
        }
    }
}

