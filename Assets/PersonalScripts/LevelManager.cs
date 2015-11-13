using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static int score;        // The player's score.
    Text text;                      // Reference to the Text component.
    public float _timer;                   // The time until the level is complete.
    private GameManager _gameManager;      // Reference to GameManager

    void Awake()
    {
        // Set up the reference.
        text = GetComponent<Text>();

        // Reset the score.
        score = 0;

        // Reset timer
        _timer = 30f;

        // Get GameManager
        _gameManager = transform.GetComponent<GameManager>();
    }

    public void AddPoints(int Amount)
    {
        score += Amount;
    }

    void FixedUpdate()
    {
        // Timer doesn't need to drop if its negative
        if (_timer > 0)
            _timer -= Time.deltaTime;
    }

    void Update()
    {
        // Set the displayed text to be the word "Score" followed by the score value.
        if (_timer > 0)
            text.text = "Score: " + score + "\tTime: " + (int)_timer;
        else
            OnGameOver();  
    }

    void OnGameOver()
    {
        text.text = "Game Over!!";
        _gameManager.AddCoins(score);
    }
}
