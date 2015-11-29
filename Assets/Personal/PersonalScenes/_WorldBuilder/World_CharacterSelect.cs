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
            // makes sure there is a game manager present
            // Note: should be present in all World_* scripts
            if (FindObjectOfType<AnimalGameManager>() == null)
            {
                _manager = gameObject.AddComponent<AnimalGameManager>();
            }
            else
            {
                _manager = FindObjectOfType<AnimalGameManager>().GetComponent<AnimalGameManager>();
            }
            // 
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
            _manager.PlayerAnimalObject = _charSelect.currentCharacter;
            _manager.Save();
            _manager.SavePetInfoFromCharacterSelect();
            GetComponent<SceneSwapper>().LoadScene("Main");
            // changes scene to the main menu
            //_swap.LoadScene(2);
            //Debug.Log(_manager._userName);
            Debug.Log(_manager._userName);
        }
    }

}
