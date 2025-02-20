using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerController player { get; private set; }
    public MiniGamePlayer miniGamePlayer { get; private set; }
    private AreaManager areaManager;
    private UIManager uiManager;
    private MiniGameResultUI miniGameResultUI;

    public int currentScore = 0;
    public int bestScore = 0;
    public bool ChangeScene = false;

    public int currentScene = 0;
    public bool isFirstLoading = true;
    public bool isRiding = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 있다면 제거
        }

    }
    private void Start()
    {
        if (currentScene == 1)
        {
            if (!isFirstLoading)
            {
                MiniGameStart();
            }
            else isFirstLoading = false;
        }
    }

    public void MiniGameStart()
    {
        currentScore = 0;
        uiManager.SetPlayGame();
        Time.timeScale = 1;
        
    }

    public void MiniGameOver()
    {
        uiManager.SetGameOver();
        Time.timeScale = 1;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("BestScore", bestScore);
        }
        uiManager.EndScore(currentScore, bestScore);
    }


    public void MiniGameExit()
    {
        isFirstLoading = true;
        SceneManager.LoadScene(0);
        currentScene = 0;
        Time.timeScale = 1;
        Transform chil = transform.GetChild(0);
        chil.gameObject.SetActive(true);
    }

    

    public void AddScore(int score)
    {
        currentScore += score;
        uiManager.UpdateScore(currentScore);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer(); // 씬이 바뀔 때마다 적절한 플레이어를 찾음
        FindArea(); // 씬이 바뀔때마다 에어리어 매니저를 찾음
        FindUI(); // 씬이 바뀔때마다 ui매니저를 찾음
    }
    private void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>(); // 일반 게임일 경우 Player 찾기
        miniGamePlayer = FindObjectOfType<MiniGamePlayer>(); // 미니게임일 경우 MinigamePlayer 찾기

        if (player != null)
        {
            player.Init(this);
            Debug.Log("플레이어를 찾음!");
        }
        else if (miniGamePlayer != null)
        {
            miniGamePlayer.Init(this);
            Debug.Log("미니게임플레이어를 찾음!");
        }
        else
        {
            Debug.LogWarning("플레이어가 없음!");
        }
    }

    private void FindArea()
    {
        areaManager = GetComponentInChildren<AreaManager>();

        if (areaManager != null)
        {
            areaManager.Init(this);
        }
        else
        {
            Debug.LogWarning("에어리어매니저가 없음!");
        }

    }

    private void FindUI()
    {
        uiManager = FindObjectOfType<UIManager>();
        miniGameResultUI = FindObjectOfType<MiniGameResultUI>(true);

        if (uiManager != null)
        {
            uiManager.Init(this);
            Debug.Log("UI매니저를 찾음!");
        }
        else if (miniGameResultUI != null)
        {
            miniGameResultUI.Init(this);
            Debug.Log("미니게임결과UI를 찾음");
        }
        else
        { Debug.LogWarning("UI매니저가 없음!"); }
    }

    private void Update()
    {
        if (miniGameResultUI != null)
        {
            if (ChangeScene == true && currentScene == 0)
            {
                miniGameResultUI.gameObject.SetActive(true);
            }
        }
    }
}
