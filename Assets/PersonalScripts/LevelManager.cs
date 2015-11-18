using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static int score;        // The player's score.
    Text text;                      // Reference to the Text component.
    public Timer _timer;                   // The time until the level is complete.
    private GameManager _gameManager;      // Reference to GameManager
    public float _levelLength = 30f;
    bool _alreadyGavePoints = false;
    void Awake()
    {
        // Set up the reference
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;
        // create new timer
        _timer = gameObject.AddComponent<Timer>();
        _timer.SetTimer(_levelLength);
        _timer._isPaused = false;
       
        // Get GameManager
        _gameManager = transform.GetComponent<GameManager>();
    }

    public void AddPoints(int Amount)
    {
        score += Amount;
    }

    void FixedUpdate()
    {
        _timer._stopTime -= Time.deltaTime;
    }

    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        if (_timer._timeUp && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth > 0)
            text.text = "Score: " + score + "\tTime: " + (int)_timer._stopTime;
        if ((int)_timer._stopTime <= 0)
            OnGameOver();  
    }

    public void OnGameOver()
    {
        text.text = "Game Over!!";
        _alreadyGavePoints = true;
        // handles glitch where player consistently gets points before returning
        // to main game
        if (!_alreadyGavePoints)
            _gameManager.AddCoins(score);
        // save points
        _gameManager.Save();
    }

    public void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void ReturnToMain()
    {
        Application.LoadLevel("Main");
    }
}
