// Project: Pet Pals
// File: World_MiniGame_02_UI.cs
// Modification History:
// Author           Date
// Jean-Baptiste	11/28/15
// Labus			11/29/15

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace PersonalScripts
{
    class World_MiniGame_02_UI: MonoBehaviour
    {
        public static World_MiniGame_02_UI SP;
        public static int score;
        public Button[] buttons;
        public GameObject optionsPanel;
        public Button pauseBtn;
        public Button restartBtn;
        public Button exitBtn;
        public Button quitBtn;
        public Button closeOptionsBtn;
        public Button tryAgainBtn;
        GameObject gameOver;
        Slider[] sliders;
        Slider volumeSlider;

        private int bestScore = 0;

        void Awake()
        {
            SP = this;
            score = 0;
            bestScore = PlayerPrefs.GetInt("BestScorePlatforms", 0);
            gameOver = GameObject.Find("ScoreText");
            gameOver.gameObject.SetActive(false);
            SetButtons();
            SetupSliders();
            SetPanels();
        }

        void OnGUI()
        {
            GUILayout.Space(3);
            //GUILayout.Label(" Score: " + score);
            //GUILayout.Label(" Highscore: " + bestScore);

            if (World_MiniGame_02.gameState == GameState.gameover)
            {
                GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));

                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();

                //GUILayout.Label("Game over!");
                if (score > bestScore)
                {
                    GUI.color = Color.red;
                    //GUILayout.Label("New highscore!");
                    GUI.color = Color.white;
                }
                //if (GUILayout.Button("Try again"))
                //{
                //    Application.LoadLevel(Application.loadedLevel);
                //}
                Text finalScore = gameOver.GetComponent<Text>();
                finalScore.text = "Game Over!" + System.Environment.NewLine + "Score: " + score;
                gameOver.gameObject.SetActive(true);
                tryAgainBtn.gameObject.SetActive(true);
                quitBtn.gameObject.SetActive(true);

                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                GUILayout.EndArea();

            }
        }

        public void CheckHighscore()
        {
            if (score > bestScore)
            {
                PlayerPrefs.SetInt("BestScorePlatforms", score);
            }
        }

        public void SetButtons()
        {
            if (buttons.Length == 0)
            {
                buttons = GameObject.FindObjectsOfType<Button>();
            }
            foreach (var button in buttons)
            {
                string buttonName = button.name;
                switch (buttonName)
                {
                    case "Options":
                        pauseBtn = button;
                        button.onClick.AddListener(delegate
                        {
                            Time.timeScale = 0;
                            pauseBtn.gameObject.SetActive(false);
                            optionsPanel.gameObject.SetActive(true);
                        });
                        break;
                    case "RestartBtn":
                        restartBtn = button;
                        button.onClick.AddListener(delegate
                        {
                            Application.LoadLevel(Application.loadedLevel);
                        });
                        break;
                    case "ExitBtn":
                        exitBtn = button;
                        button.onClick.AddListener(delegate
                        {
                            World_MiniGame_02 wmg2 = GameObject.Find("Main Camera").GetComponentInChildren<World_MiniGame_02>();
                            wmg2.GameOver();
                            Time.timeScale = 1;
                            Application.LoadLevel("Main");
                        });
                        break;
                    case "CloseOptionsBtn":
                        closeOptionsBtn = button;
                        button.onClick.AddListener(delegate
                        {
                            Time.timeScale = 1;
                            optionsPanel.gameObject.SetActive(false);
                            pauseBtn.gameObject.SetActive(true);
                        });
                        break;
                    case "TryAgainBtn":
                        tryAgainBtn = button;
                        tryAgainBtn.gameObject.SetActive(false);
                        button.onClick.AddListener(delegate
                        {
                            tryAgainBtn.gameObject.SetActive(false);
                            Application.LoadLevel(Application.loadedLevel);
                        });
                        break;
                    case "QuitBtn":
                        quitBtn = button;
                        quitBtn.gameObject.SetActive(false);
                        button.onClick.AddListener(delegate
                        {
                            World_MiniGame_02 wmg2 = GameObject.Find("Main Camera").GetComponentInChildren<World_MiniGame_02>();
                            wmg2.GameOver();
                            Time.timeScale = 1;
                            //GameObject.FindObjectOfType<Canvas>().GetComponent<LevelManager>().ReturnToMain();
                            Application.LoadLevel("Main");
                        });
                        break;
                }
            }
        }

        public void SetPanels()
        {
            if (optionsPanel == null)
            {
                optionsPanel = GameObject.FindGameObjectWithTag("Panel");
                optionsPanel.gameObject.SetActive(false);
            }
        }

        public void SetupSliders()
        {
            sliders = GameObject.FindObjectsOfType<Slider>();
            foreach (var slider in sliders)
            {
                string sliderName = slider.name;
                switch (sliderName)
                {
                    case "VolumeSlider":
                        volumeSlider = slider;
                        slider.onValueChanged.AddListener(delegate
                        {
                            AudioSource music = GameObject.Find("Main Camera").GetComponentInChildren<AudioSource>();
                            music.volume = volumeSlider.value;
                        });
                        break;
                }
            }
        }
    }
}
