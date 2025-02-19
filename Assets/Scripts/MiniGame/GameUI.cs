using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI scoreText;
    protected override UIState GetUIState()
    {
        return UIState.Game;
    }

    public void SetUI(int score)
    {
        scoreText.text = score.ToString();
    }

}
