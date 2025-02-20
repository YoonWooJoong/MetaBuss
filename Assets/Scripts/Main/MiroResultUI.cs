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
    private void OnSuccessButton() // 성공확인버튼 누를시 ui비활성화
    {
         successUI.gameObject.SetActive(false);
    }

    private void OnFailButton() // 실패 확인 버튼 누를시 ui 비활성화
    {
        failUI.gameObject.SetActive(false);
    }

    public void TimeRemainText(float timer)
    {
        timeText.text = timer.ToString("N2");
    }

    private void Start()
    {
        successUIButton.onClick.AddListener(OnSuccessButton); // 버튼 할당
        failUIButton.onClick.AddListener(OnFailButton);
    }
    
    

}
