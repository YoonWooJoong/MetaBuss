using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    Home,
    Game,
    GameOver,
}

public class UIManager : MonoBehaviour
{
    HomeUI homeUI;
    GameUI gameUI;
    GameOverUI gameOverUI;
    GameManager gameManager;
    private UIState currentState;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Start()
    {
        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);
        if (gameManager.isFirstLoading == true) // 리스타트시 home으로 돌아가는 문제 발생해서 수정함
        {
            ChangeState(UIState.Home);
        }
        else { ChangeState(UIState.Game); }
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(UIState.GameOver);
    }

    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }
    public void UpdateScore(int score)
    {
        gameUI.SetUI(score);
    }

    public void EndScore(int score, int bestScore)
    {
        gameOverUI.SetUI(score, bestScore);
    }
}
