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

    void Update()
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
}
