using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace PersonalScripts
{
    public class LevelManager : MonoBehaviour, IMinigameLevelManager
    {

        public static int _score;        // The player's score.
        Text text;                      // Reference to the Text component.
        private AnimalGameManager _gameManager;      // Reference to GameManager
        public float _levelLength = 30f;
        protected bool _alreadyGavePoints = false;

        public float levelLength { get { return (_levelLength); } set { _levelLength = (value); } }
        public int score { get { return (_score); } set { _score = (value); } }

        public virtual void OnGameOver()
        {

        }
        public void AddPoints(int Amount)
        {
            _score += Amount;
        }

        public virtual void Awake()
        {
            // Set up the reference
            text = GetComponent<Text>();

            // Reset the score.
            _score = 0;
            // create new timer
            /*
            _timer = gameObject.AddComponent<Timer>();
            _timer.SetTimer(_levelLength);
            _timer._isPaused = false;
            */
            // Get GameManager
            _gameManager = transform.GetComponent<AnimalGameManager>();
        }       

        public void RestartLevel()
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        public void ReturnToMain()
        {
            Application.LoadLevel("Main");
        }

        public void ReturnToStartMenu()
        {
            Application.LoadLevel("StartMenu");
        }

        public void GoToCharacterSelect()
        {
            Application.LoadLevel("CharacterSelect");
        }
    }

}
