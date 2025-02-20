using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReaderBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI readerBoardScore;
    [SerializeField] private TextMeshProUGUI readerBoardTime;
    [SerializeField] private TextMeshProUGUI readerBoardCount;

    private void Update()
    {
        readerBoardScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        readerBoardTime.text = PlayerPrefs.GetFloat("BestTime").ToString("N2");
        readerBoardCount.text = PlayerPrefs.GetInt("ClearCount").ToString();
    }
}
