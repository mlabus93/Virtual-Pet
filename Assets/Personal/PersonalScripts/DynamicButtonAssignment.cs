using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PersonalScripts
{
    public class DynamicButtonAssignment : MonoBehaviour
    {
        // container for all buttons in ui
        public Button[] _buttons;
        public IAnimalCharacter _player;

        // Use this for initialization
        void Start()
        {
            _buttons = gameObject.GetComponentsInChildren<Button>();
            SetPlayer();
            SetupButtons();
        }

        private void SetPlayer()
        {
            // clears player reference
            _player = null;
            // determines Player animal type, there should only be 1 player
            // there could possibly be more than 1 animal
            GameObject holder = GameObject.FindGameObjectWithTag("Player");

            // if-else structure to determine exact animal type
            if (_player == null)
            {
                _player = holder.GetComponent<CatCharacter>();
                if (_player == null)
                {
                    _player = holder.GetComponent<DogCharacter>();
                    if (_player == null)
                    {
                        _player = holder.GetComponent<RabbitCharacter>();
                        if (_player == null)
                        {
                            _player = holder.GetComponent<FoxCharacter>();
                            if (_player == null)
                            {
                                _player = holder.GetComponent<PenguinCharacter>();
                                if (_player == null)
                                {
                                    _player = holder.GetComponent<PandaCharacter>();
                                    // if player has not been assigned by this point there was an error
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("NO PLAYER OBJECT FOUND!");
            }
            Debug.Log("Player found: " + _player.GetNickName());
        }

        public virtual void SetupButtons()
        {
            
        }

        void PossiblyWork()
        {
            Debug.Log("IT WORKS FOR IN BUTTON FUNCTIONS");
        }
        // Update is called once per frame
        void Update()
        {

        }
    }

}
