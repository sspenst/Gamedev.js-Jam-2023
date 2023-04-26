using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;
    public bool isGameOver;
    public bool hasPowerup;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHighScoreText;

    public GameObject gameOverPanel;
    public GameObject newHighScoreText;
    
    void Start()
    {
        score = 0;
        isGameOver = false;
        hasPowerup = false;
        gameOverPanel = GameObject.Find("GameOverPanel");

        gameOverPanel.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameOver = true;

        bool newHighScore = score > Database.Instance.HighScore;

        if (newHighScore)
        {
            Database.Instance.HighScore = score;
            Database.Instance.SaveDatabase();
        }

        gameOverScoreText.text = "Score: " + score;
        gameOverHighScoreText.text = "High Score: " + Database.Instance.HighScore;
        gameOverPanel.SetActive(true);
        newHighScoreText.SetActive(newHighScore);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
