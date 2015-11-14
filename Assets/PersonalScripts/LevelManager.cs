using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static int score;        // The player's score.
    Text text;                      // Reference to the Text component.
    public Timer _timer;                   // The time until the level is complete.
    private GameManager _gameManager;      // Reference to GameManager

    void Awake()
    {
        // Set up the reference
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;
        // create new timer
        _timer = gameObject.AddComponent<Timer>();
        _timer.SetTimer(30f);
        // Get GameManager
        _gameManager = transform.GetComponent<GameManager>();
    }

    public void AddPoints(int Amount)
    {
        score += Amount;
    }

    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        if (_timer._timeUp)
            text.text = "Score: " + score + "\tTime: " + (int)_timer.GetTimeLeft();
        else
            OnGameOver();  
    }

    void OnGameOver()
    {
        text.text = "Game Over!!";
        _gameManager.AddCoins(score);
    }

}
