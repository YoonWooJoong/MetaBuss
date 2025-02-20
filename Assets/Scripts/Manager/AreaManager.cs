using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private List<Rect> eventAreas; // 이벤트를 실행할 지역 생성

    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상
    [SerializeField] private TextMeshProUGUI EscapeTimerText;

    private TextAnimation textAnimation;

    public bool IsEnterMiro = false;
    public bool IsEscapeSuccess = false;
    public int EscaepSuccessCount = 0;
    public float EscapeTimer = 60f;

    GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        textAnimation = GetComponent<TextAnimation>();
    }

    private void OnDrawGizmosSelected()
    {
        if (eventAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in eventAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            // 초기 x값에 넓이의 반을 나눈것을 더하면 x의 중심점 y값도 똑같다.
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    private void GoToMiniGame(GameManager gameManager)
    {
        if (eventAreas.Count == 0)
        {
            Debug.LogError("eventAreas가 설정되지 않았습니다.");
            return;
        }
        for (int i = 0; i < eventAreas.Count; i++)
        {
            if (eventAreas[0].Contains(gameManager.player.transform.position) == true)
            {
                SceneManager.LoadScene("MiniGame");
                gameManager.currentScene = 1;
                gameManager.ChangeScene = true;
                Time.timeScale = 0;
                this.gameObject.SetActive(false);
            }
            if (eventAreas[1].Contains(gameManager.player.transform.position) == true)
            {
                gameManager.player.transform.position = new Vector3(-58.45f, 7.45f, 0);
                IsEnterMiro = true;
                IsEscapeSuccess = false;
                EscapeTimer = 10f;
            }
            if (eventAreas[2].Contains(gameManager.player.transform.position) == true)
            {
                gameManager.player.transform.position = new Vector3(0, 0, 0);
                EscaepSuccessCount++;
                IsEscapeSuccess = true; // 결과 표출 후에 false로 변경
                IsEnterMiro = false;
            }
        }
        
    }
    private void Update()
    {
        GoToMiniGame(gameManager);
        if (IsEnterMiro)
        {
            if (EscapeTimer <= 0 && (IsEscapeSuccess == false))
            {
                gameManager.player.transform.position = new Vector3(0, 0, 0);
                IsEnterMiro = false;
            }
            if (EscapeTimer <= 0)
            {
                EscapeTimer = 0;
            }
            else 
            { 
                EscapeTimer -= Time.deltaTime;
                EscapeTimerText.text = EscapeTimer.ToString("N2");
            }
            if (EscapeTimer <= 5)
            {
                textAnimation.Hurry();
            }
            else { textAnimation.NoHurry(); }
            
        }
    }


}
