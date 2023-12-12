using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    [Header("Score")]
    int score = 0;
    private List<int> highScores;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text currentScoreDisplay;
    [SerializeField] private TMP_Text[] highscoreArray = new TMP_Text[5];
    [SerializeField] private GameObject finalScoreCanvas;

    [Header("Game State")]
    private bool isFirstRun;
    public bool isGameOver;


    void Awake()
    {
        isGameOver = false;
        isFirstRun = true;

        finalScoreCanvas.SetActive(false);
        highScores = new List<int>();
    }

    private void Start()
    {
        LoadHighscores();
    }

    void Update()
    {
        if (isGameOver)
        {
            finalScoreCanvas.SetActive(true);
            currentScoreDisplay.text = "Your Score: " + score.ToString();
        }

        if (isGameOver && isFirstRun)
        {
            GameOver();
            isFirstRun = false;
        }
    }

    public void GameOver()
    {
        AddHighscore(score);
        UpdateUI();
    }

    public void IncreaseScore(int addScore)
    {
        score += addScore;
        scoreText.text = score.ToString("0000");
    }

    public void DisplayHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            highscoreArray[i].text = (i + 1).ToString() + ". " + highScores[i].ToString();
        }
    }

    public void SaveHighscores()
    {
        string json = JsonUtility.ToJson(new HighscoreList { highscoreEntries = highScores });
        PlayerPrefs.SetString("Highscores", json);
        PlayerPrefs.Save();
    }
    // Add the player's score to the highscores and update the UI

    public void LoadHighscores()
    {
        string json = PlayerPrefs.GetString("Highscores", "");
        if (!string.IsNullOrEmpty(json))
        {
            HighscoreList loadedHighscores = JsonUtility.FromJson<HighscoreList>(json);
            if (loadedHighscores != null)
            {
                highScores = loadedHighscores.highscoreEntries;
            }
        }
    }// Load existing highscores from PlayerPrefs

    private void OnDisable()
    {
        
        SaveHighscores();
    }// Save highscores when the game is disabled or closed

    [System.Serializable]
    public class HighscoreList
    {
        public List<int> highscoreEntries;
    }

    void AddHighscore(int newScore)
    {
        highScores.Add(newScore);
        highScores.Sort();
        highScores.Reverse();

        while (highScores.Count > 5)
        {
            highScores.RemoveAt(5);
        }
    }

    void UpdateUI()
    {
        DisplayHighScores();
    }
}
