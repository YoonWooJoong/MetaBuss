using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameResultUI : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button okButton;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void GetBestScore(int bestScore)
    {
        bestScoreText.text = bestScore.ToString();
    }

    private void Start()
    {
        GetBestScore(gameManager.bestScore);
        okButton.onClick.AddListener(OnClickOkButton);
    }

    private void OnClickOkButton()
    {
        this.gameObject.SetActive(false);
        gameManager.ChangeScene = false;
    }
}
