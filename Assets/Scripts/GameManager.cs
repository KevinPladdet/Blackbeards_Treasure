using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [Header("Score")]
    [SerializeField] private int totalScore;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI piratesScoreText;
    [SerializeField] private TextMeshProUGUI cannonballScoreText;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    [Header("End Menus")]
    [SerializeField] private GameObject endMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject levelCompletedMenu;

    public bool pausedGame;

    public int amountCannonballs;
    public int piratesAlive;
    public int piratesKilled;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
        scoreText.text = "Score: " + totalScore;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        pausedGame = true;
        endMenu.SetActive(true);
        gameOverMenu.SetActive(true);
        piratesScoreText.text = "" + piratesKilled + " x 100";
        cannonballScoreText.text = "" + amountCannonballs + " x 200";
        totalScore += amountCannonballs * 200;
        totalScoreText.text = "" + totalScore;
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0f;
        pausedGame = true;
        endMenu.SetActive(true);
        levelCompletedMenu.SetActive(true);
        piratesScoreText.text = "" + piratesKilled + " x 100";
        cannonballScoreText.text = "" + amountCannonballs + " x 200";
        totalScore += amountCannonballs * 200;
        totalScoreText.text = "" + totalScore;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1f;
        pausedGame = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
