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

    //Ż�� ���� UIǥ��
    public void SuccessUIResult()
    {
        successUI.SetActive(true);
    }
    //Ż�� ���� UIǥ��
    public void FailUIResult()
    {
        failUI.SetActive(true);
    }
    private void OnSuccessButton() // ����Ȯ�ι�ư ������ ui��Ȱ��ȭ
    {
         successUI.gameObject.SetActive(false);
    }

    private void OnFailButton() // ���� Ȯ�� ��ư ������ ui ��Ȱ��ȭ
    {
        failUI.gameObject.SetActive(false);
    }

    public void TimeRemainText(float timer)
    {
        timeText.text = timer.ToString("N2");
    }

    private void Start()
    {
        successUIButton.onClick.AddListener(OnSuccessButton); // ��ư �Ҵ�
        failUIButton.onClick.AddListener(OnFailButton);
    }
    
    

}
