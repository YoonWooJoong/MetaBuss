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
    [SerializeField] private TextMeshProUGUI EscapeTimerText; // 탈출시간 텍스트에 실제 시간
    [SerializeField] private GameObject EscapeText; // 전체 탈출시간텍스트
    [SerializeField] private MiroResultUI resultUI; // 결과 창

    private TextAnimation textAnimation; // 시간 텍스트 애니메이션

    public bool IsEnterMiro = false; // 미로에 들어갔는지 여부
    public bool IsEscapeSuccess = false; // 미로 탈출 성공 여부
    public int EscaepSuccessCount = 0; // 미로 탈출 횟수
    public float EscapeTimer = 60f; // 미로 타이머
    private float BestTime = 0f; // 미로 탈출 최고 시간

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
                SceneManager.LoadScene("MiniGame"); // 미니게임 씬으로 넘어감
                gameManager.currentScene = 1; // 씬을 변경했는지 판단하기위한 변수
                gameManager.ChangeScene = true; // 복귀때 결과창 표출을위한 변수
                Time.timeScale = 0; // 시간 정지 
            }
            if (eventAreas[1].Contains(gameManager.player.transform.position) == true) // 미로 입장
            {
                gameManager.player.transform.position = new Vector3(-58.45f, 7.45f, 0); // 미로 시작지점
                IsEnterMiro = true; 
                IsEscapeSuccess = false;
                EscapeTimer = 60f;
                EscapeText.gameObject.SetActive(true); // 미로 타이머UI 활성
            }
            if (eventAreas[2].Contains(gameManager.player.transform.position) == true) // 미로 탈출 성공
            {
                gameManager.player.transform.position = new Vector3(0, 0, 0); // 메인처음화면으로 
                EscaepSuccessCount++; // 탈출 카운터 +1
                IsEscapeSuccess = true; // 결과 표출 후에 false로 변경
                IsEnterMiro = false; // 미로 밖이니까 flase로 변경
                EscapeText.gameObject.SetActive(false); // 밖으로 나왔으니 시간 텍스트는 비활성화
                resultUI.SuccessUIResult(); // 성공결과창 표출
                resultUI.TimeRemainText(EscapeTimer); // 시간 표출
                if (BestTime < EscapeTimer) // 최고시간이 탈출 시간보다 작으면 변경
                {
                    BestTime = EscapeTimer; 
                    PlayerPrefs.SetFloat("BestTime", BestTime);
                }
                PlayerPrefs.SetInt("ClearCount", EscaepSuccessCount); // 클리어 카운트 저장
            }
        }
        
    }
    private void Update()
    {
        GoToMiniGame(gameManager);
        if (IsEnterMiro) // 미로 활성화 일시
        {
            if (EscapeTimer <= 0 && (IsEscapeSuccess == false)) // 시간이 끝났고 탈출 실패일시 
            {
                gameManager.player.transform.position = new Vector3(0, 0, 0); // 처음화면으로 돌아감
                IsEnterMiro = false; // 미로 밖으로 나왔으니 false
                EscapeText.gameObject.SetActive(false); // 시간 텍스트 비활성화
                resultUI.FailUIResult(); // 실패 UI활성
            }
            if (EscapeTimer <= 0)
            {
                EscapeTimer = 0; // 타이머가 -로 가지않게 하기위한 처리
            }
            else 
            { 
                EscapeTimer -= Time.deltaTime;
                EscapeTimerText.text = EscapeTimer.ToString("N2"); // 소수점 2째자리까지 표출
            }
            if (EscapeTimer <= 20) // 20초보다 작으면 애니메이션 변경
            {
                textAnimation.Hurry();
            }
            else { textAnimation.NoHurry(); }
            
        }
    }


}
