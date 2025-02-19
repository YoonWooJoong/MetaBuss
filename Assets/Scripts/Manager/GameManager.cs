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

    [SerializeField]private int currentScore = 0;
    [SerializeField]private int bestScore = 0;


    public int currentScene = 0;
    public bool isFirstLoading = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� ����
        }
        else
        {
            Destroy(gameObject); //�̹� �ν��Ͻ��� �ִٸ� ����
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
        uiManager.SetPlayGame();
        Time.timeScale = 1;
        
    }

    public void MiniGameOver()
    {
        uiManager.SetGameOver();
        Time.timeScale = 1;
        if(currentScore > bestScore)
            bestScore = currentScore;
        currentScore = 0;
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
        FindPlayer(); // ���� �ٲ� ������ ������ �÷��̾ ã��
        FindArea(); // ���� �ٲ𶧸��� ����� �Ŵ����� ã��
        FindUI(); // ���� �ٲ𶧸��� ui�Ŵ����� ã��
    }
    private void FindPlayer()
    {
        player = FindObjectOfType<PlayerController>(); // �Ϲ� ������ ��� Player ã��
        miniGamePlayer = FindObjectOfType<MiniGamePlayer>(); // �̴ϰ����� ��� MinigamePlayer ã��

        if (player != null)
        {
            player.Init(this);
            Debug.Log("�÷��̾ ã��!");
        }
        else if (miniGamePlayer != null)
        {
            miniGamePlayer.Init(this);
            Debug.Log("�̴ϰ����÷��̾ ã��!");
        }
        else
        {
            Debug.LogWarning("�÷��̾ ����!");
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
            Debug.LogWarning("�����Ŵ����� ����!");
        }

    }

    private void FindUI()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.Init(this);
            Debug.Log("UI�Ŵ����� ã��!");
        }
        else
        { Debug.LogWarning("UI�Ŵ����� ����!"); }
    }
}
