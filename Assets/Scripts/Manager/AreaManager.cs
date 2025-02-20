using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private List<Rect> eventAreas; // �̺�Ʈ�� ������ ���� ����

    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f); // ����� ����
    [SerializeField] private TextMeshProUGUI EscapeTimerText; // Ż��ð� �ؽ�Ʈ�� ���� �ð�
    [SerializeField] private GameObject EscapeText; // ��ü Ż��ð��ؽ�Ʈ
    [SerializeField] private MiroResultUI resultUI; // ��� â

    private TextAnimation textAnimation; // �ð� �ؽ�Ʈ �ִϸ��̼�

    public bool IsEnterMiro = false; // �̷ο� ������ ����
    public bool IsEscapeSuccess = false; // �̷� Ż�� ���� ����
    public int EscaepSuccessCount = 0; // �̷� Ż�� Ƚ��
    public float EscapeTimer = 60f; // �̷� Ÿ�̸�
    private float BestTime = 0f; // �̷� Ż�� �ְ� �ð�

    GameManager gameManager;


    private void Start()
    {
        this.gameManager = GameManager.instance;
        textAnimation = GetComponent<TextAnimation>();
    }

    private void OnDrawGizmosSelected()
    {
        if (eventAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in eventAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            // �ʱ� x���� ������ ���� �������� ���ϸ� x�� �߽��� y���� �Ȱ���.
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    private void GoToMiniGame(GameManager gameManager)
    {
        if (eventAreas.Count == 0)
        {
            Debug.LogError("eventAreas�� �������� �ʾҽ��ϴ�.");
            return;
        }
        for (int i = 0; i < eventAreas.Count; i++)
        {
            if (eventAreas[0].Contains(gameManager.player.transform.position) == true)
            {
                SceneManager.LoadScene("MiniGame"); // �̴ϰ��� ������ �Ѿ
                gameManager.currentScene = 1; // ���� �����ߴ��� �Ǵ��ϱ����� ����
                gameManager.ChangeScene = true; // ���Ͷ� ���â ǥ�������� ����
                Time.timeScale = 0; // �ð� ���� 
            }
            if (eventAreas[1].Contains(gameManager.player.transform.position) == true) // �̷� ����
            {
                gameManager.player.transform.position = new Vector3(-58.45f, 7.45f, 0); // �̷� ��������
                IsEnterMiro = true; 
                IsEscapeSuccess = false;
                EscapeTimer = 60f;
                EscapeText.gameObject.SetActive(true); // �̷� Ÿ�̸�UI Ȱ��
            }
            if (eventAreas[2].Contains(gameManager.player.transform.position) == true) // �̷� Ż�� ����
            {
                gameManager.player.transform.position = new Vector3(0, 0, 0); // ����ó��ȭ������ 
                EscaepSuccessCount++; // Ż�� ī���� +1
                IsEscapeSuccess = true; // ��� ǥ�� �Ŀ� false�� ����
                IsEnterMiro = false; // �̷� ���̴ϱ� flase�� ����
                EscapeText.gameObject.SetActive(false); // ������ �������� �ð� �ؽ�Ʈ�� ��Ȱ��ȭ
                resultUI.SuccessUIResult(); // �������â ǥ��
                resultUI.TimeRemainText(EscapeTimer); // �ð� ǥ��
                if (BestTime < EscapeTimer) // �ְ�ð��� Ż�� �ð����� ������ ����
                {
                    BestTime = EscapeTimer; 
                    PlayerPrefs.SetFloat("BestTime", BestTime);
                }
                PlayerPrefs.SetInt("ClearCount", EscaepSuccessCount); // Ŭ���� ī��Ʈ ����
            }
        }
        
    }
    private void Update()
    {
        GoToMiniGame(gameManager);
        if (IsEnterMiro) // �̷� Ȱ��ȭ �Ͻ�
        {
            if (EscapeTimer <= 0 && (IsEscapeSuccess == false)) // �ð��� ������ Ż�� �����Ͻ� 
            {
                gameManager.player.transform.position = new Vector3(0, 0, 0); // ó��ȭ������ ���ư�
                IsEnterMiro = false; // �̷� ������ �������� false
                EscapeText.gameObject.SetActive(false); // �ð� �ؽ�Ʈ ��Ȱ��ȭ
                resultUI.FailUIResult(); // ���� UIȰ��
            }
            if (EscapeTimer <= 0)
            {
                EscapeTimer = 0; // Ÿ�̸Ӱ� -�� �����ʰ� �ϱ����� ó��
            }
            else 
            { 
                EscapeTimer -= Time.deltaTime;
                EscapeTimerText.text = EscapeTimer.ToString("N2"); // �Ҽ��� 2°�ڸ����� ǥ��
            }
            if (EscapeTimer <= 20) // 20�ʺ��� ������ �ִϸ��̼� ����
            {
                textAnimation.Hurry();
            }
            else { textAnimation.NoHurry(); }
            
        }
    }


}
