using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    private static ScoreManager instance;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text highScoreText;

    private int currentScore = 0;
    private int highScore = 0;

    public static ScoreManager Instance { get { return instance; } }

    #region Mono

    private void Awake()
    {
        // If Singleton istance is null = add this
        if (instance == null) instance = this;
        else Debug.LogError("ERROR: There only can be one ScoreManager instance in scene! Remove redundand ScoreManager instances");
    }

    private void Start()
    {
        //Get HighScore from PlayerPrefs
        highScore = GetHighScoreFromPlayerPrefs();

        //Update UI
        UpdateScoreText();
        UpdateHighScoreText();
    }

    #endregion

    #region Public Methods

    public void HandleCoinCollected()
    {
        AddScore(10); //Add 10 score
    }

    #endregion


    #region Private Methods
    private void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd; 
        UpdateScoreText(); //Update UI score text

        //If current score is higher than highscore save it to playerprefs
        if (currentScore > highScore)
        {
            highScore = currentScore; //Save currentScore to highscore var
            UpdateHighScoreText(); //Update UI highscore
            PlayerPrefs.SetInt(HighScoreKey, highScore); //save permanently to disk
        }
    }

    /// <summary>
    /// Loads Highscore directly from player prefs
    /// </summary>
    /// <returns></returns>
    private int GetHighScoreFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(HighScoreKey)) //Check if key exists in PlayerPrefs
        {
            return PlayerPrefs.GetInt(HighScoreKey); //Get int value from PlayerPrefs
        }

        return 0;
    }

    /// <summary>
    /// Updates Score Text UI
    /// </summary>
    private void UpdateScoreText()
    {
        scoreText.text = string.Format("Score: {0}", currentScore);
    }

    /// <summary>
    /// Updates High Score Text UI
    /// </summary>
    private void UpdateHighScoreText()
    {
        highScoreText.text = string.Format("High Score: {0}", highScore);
    }

    #endregion
}
