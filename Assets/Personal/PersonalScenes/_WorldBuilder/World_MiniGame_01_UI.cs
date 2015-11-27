using UnityEngine;
using System.Collections;
using PersonalScripts;
using UnityEngine.UI;


class World_MiniGame_01_UI : DynamicButtonAssignment
{
    public override void SetupButtons()
    {
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
                    _player.RotateThroughFits();
                });
            }
        }
    }
}

