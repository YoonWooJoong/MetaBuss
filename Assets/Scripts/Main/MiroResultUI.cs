using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiroResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject successUI;
    [SerializeField] private GameObject failUI;
    [SerializeField] private Button successUIButton;
    [SerializeField] private Button failUIButton;

    //탈출 성공 UI표출
    public void SuccessUIResult()
    {
        successUI.SetActive(true);
    }
    //탈출 실패 UI표출
    public void FailUIResult()
    {
        failUI.SetActive(true);
    }
    private void OnSuccessButton()
    {
         successUI.gameObject.SetActive(false);
    }

    private void OnFailButton()
    {
        failUI.gameObject.SetActive(false);
    }

    public void TimeRemainText(float timer)
    {
        timeText.text = timer.ToString("N2");
    }

    private void Start()
    {
        successUIButton.onClick.AddListener(OnSuccessButton);
        failUIButton.onClick.AddListener(OnFailButton);
    }
    
    

}
