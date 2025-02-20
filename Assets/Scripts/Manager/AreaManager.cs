using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private List<Rect> eventAreas; // 이벤트를 실행할 지역 생성

    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    GameManager gameManager;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
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
            }
        }
        
    }
    private void Update()
    {
        GoToMiniGame(gameManager);
    }


}
