using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameObserver : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameClearText;
    [SerializeField] Text scoreText;

    const int MAX_SCORE = 9999;
    int score = 0;
    public bool GameFinish;

    public static GameObserver gameObserver;
    private void Awake()
    {
        gameObserver = this;
    }

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int val)
    {
        score += val;
        if(score > MAX_SCORE)
        {
            score = MAX_SCORE;
        }
        UpdateScoreText();
    }

    public void GameOver()
    {
        SoundObserver.soundObserver.PlaySE(SoundObserver.SE.GameOver);
        gameOverText.SetActive(true);
        GameStopLoadScene();
    }

    public void GameClear()
    {
        SoundObserver.soundObserver.PlaySE(SoundObserver.SE.GameClear);
        gameClearText.SetActive(true);
        GameStopLoadScene();
    }

    void GameStopLoadScene()
    {
        GameFinish = true;
        Invoke("RestartScene", 1.5f);
    }

    void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void UpdateScoreText()
    {
        scoreText.text = "SCORE:" + score;
    }
}
