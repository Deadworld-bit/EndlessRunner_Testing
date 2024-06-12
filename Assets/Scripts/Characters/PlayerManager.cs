using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool gameStart;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameStartPanel;

    public static int points;
    [SerializeField] private TextMeshProUGUI coinsVisual;

    void Start()
    {
        gameOver = false;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        gameStart = false;
        points = 0;
    }

    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (SwipeManager.tap)
        {
            gameStart = true;
            gameStartPanel.SetActive(false);
        }

        coinsVisual.text = "Coins: " + points;
    }
}
