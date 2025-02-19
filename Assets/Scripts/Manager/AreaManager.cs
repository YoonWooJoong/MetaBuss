using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private List<Rect> eventAreas; // �̺�Ʈ�� ������ ���� ����

    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f); // ����� ����

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
        foreach (var area in eventAreas)
        {
            if (area.Contains(gameManager.player.transform.position) == true)
            {
                SceneManager.LoadScene("MiniGame");
                gameManager.currentScene = 1;
                Time.timeScale = 0;
                this.gameObject.SetActive(false);
            }  
        }
        
    }
    private void Update()
    {
        GoToMiniGame(gameManager);
    }


}
