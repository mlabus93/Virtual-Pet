using UnityEngine;
using System.Collections;

namespace PersonalScripts
{
    public class World_CharacterSelect : MonoBehaviour
    {

        AnimalGameManager _manager;
        CharacterSelection _charSelect;
        SceneSwapper _swap;
        // Use this for initialization
        void Start()
        {
            _manager = FindObjectOfType<AnimalGameManager>();
            _charSelect = FindObjectOfType<CharacterSelection>().GetComponent<CharacterSelection>();
           // swap = gameObject.AddComponent<SceneSwapper>();
        }

        void Update()
        {
            if (_manager == null)
            {
                _manager = FindObjectOfType<AnimalGameManager>();
                Debug.Log("It was null");
            }
            //Debug.Log("its not null anymore");
        }       

        public void CharacterSelected()
        {
            // stores animal choice, resets statuses and saves
            AnimalGameManager._player = _charSelect.iAnimal;
            AnimalGameManager._player.ResetStatuses();
            _manager.PlayableAnimal = _charSelect.currentCharacter;
            _manager.Save();
            // changes scene to the main menu
            //_swap.LoadScene(2);
            //Debug.Log(_manager._userName);
            Debug.Log(_manager._userName);
        }
    }

}
