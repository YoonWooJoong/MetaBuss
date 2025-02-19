using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // ���ع��� ��ü������ ���� ���ð����� �Ʒ��� ���ð����� ��ġ ����
    private float highPosY = 1f; 
    private float lowPosY = -1f;
    // ���ع��� ���ۻ�����
    private float holeSizeMin = 1f;
    private float holeSizeMax = 3f;

    [SerializeField] private Transform topObject;
    [SerializeField] private Transform bottomObject;

    public float widthPadding = 4f;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax); // ���� ����� �������� ����
        float halfHoleSize = holeSize / 2; // ���� ���� ������ ����

        topObject.localPosition = new Vector3(0, halfHoleSize); // ������ �������� �������� �������������� ���οø�
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // �Ʒ��� ����

        // ���� ������ ���̱� ������ ������ �������� widthPadding�� ���ؼ� ��������
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY); // ��ü������ ���Ʒ� ������ ������ġ�� �������� ������

        transform.position = placePosition; // �������� �����ǿ� ���� ������ ���� �־���

        return placePosition; // ��ġ ��ȯ
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MiniGamePlayer player = collision.GetComponent<MiniGamePlayer>();
        if (player != null)
        {
            gameManager.AddScore(1);
        }
    }



}
