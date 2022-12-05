using BaseTemplate.Behaviours;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { START, PLAY, END }

public class GameManager : MonoSingleton<GameManager>
{
    public List<PlayerController> _players;

    public GameState gameState;
    void Awake()
    {
        gameState = GameState.START;

        RaceManager.Instance.Init();

        UIManager.Instance.Init();
    }

    void Update()
    {
        if (Input.anyKey && gameState == GameState.START)
        {
            StartGame();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    void StartGame()
    {
        gameState = GameState.PLAY;

        foreach (PlayerController player in _players)
        {
            player.Init();
        }



        UIManager.Instance.StartGame();
    }

    public void EndGame()
    {
        gameState = GameState.END;

        foreach (PlayerController player in _players)
        {
            player.Init();
        }

        UIManager.Instance.EndGame();



    }
}
