using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReaderBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI readerBoardScore;

    private void Update()
    {
        readerBoardScore.text = PlayerPrefs.GetInt("BestScore").ToString();
    }
}
