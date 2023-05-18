using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Text ScoreDisplay;
    private int score = 0;
    private int highScore = 0;
    

    [SerializeField] private Text LivesDisplay;
    [SerializeField] private GameObject titleScreen = null;
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private GameObject GameUI = null;
    [SerializeField] private Text HighScoreDisplay;
    [SerializeField] private Text Date;
    [SerializeField] private GameObject pauseScreen;
    void Start()
    {
       GameUI.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        HighScoreDisplay.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        Date.text = PlayerPrefs.GetString("Date");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int EnemyScore)
    {
        score += EnemyScore;
        ScoreDisplay.text = "Score: " + score;
        CheckForHighScore();
    }

    public void UpdateLives(int Health)
    {
        LivesDisplay.text = "Lives: " + Health;
    }
    public void HideTitleScreen()
    {
        GameUI.SetActive(true);
        titleScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        score = 0;
        ScoreDisplay.text = "Score: " + score;
        UpdateLives(3);
    }

    public void ShowTitleScreen()
    {
        GameUI.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    public void CheckForHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            HighScoreDisplay.text = "High Score: " + highScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.SetString("Date", "Date: "+System.DateTime.Now.ToString("dd/MM/yyyy"));
            Date.text = "Date: "+System.DateTime.Now.ToString("dd/MM/yyyy");
        }
    }

}
