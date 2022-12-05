using BaseTemplate.Behaviours;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { START, PLAY, END }

public class GameManager : MonoSingleton<GameManager>
{

    public GameState gameState;
    void Awake()
    {
        gameState = GameState.START;

        RaceManager.Instance.Init();

        UIManager.Instance.Init();

        CameraSwitch.Instance.Init();
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

        PlayerController.Instance.Init();

        CameraSwitch.Instance.switchCamera("GameCamera");

        UIManager.Instance.StartGame();
    }

    public void EndGame()
    {
        gameState = GameState.END;

        PlayerController.Instance.EndRace();

        UIManager.Instance.EndGame();

        CameraSwitch.Instance.switchCamera("EndCamera");

    }
}
