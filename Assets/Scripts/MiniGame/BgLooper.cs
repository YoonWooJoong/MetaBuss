using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    private int numBgCount = 5; // ��� ī��Ʈ
    private int obstacleCount = 0; // ���ع� ī��Ʈ
    private Vector3 obstacleLastPosition = Vector3.zero; // ���ع��� ������ ��ġ

    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); // ���ع� ������Ʈ�� ã�� �迭�� ����
        obstacleLastPosition = obstacles[0].transform.position; // �� ù��° ���ع��� ��ġ�� ��������ġ�� ����
        obstacleCount = obstacles.Length; // ���ع��� ������ŭ�� ī��Ʈ�� ����

        for (int i = 0; i < obstacleCount; i++) // ī��Ʈ��ŭ �ݺ��ؼ� ���ع� ��ġ�� ��� ��������
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround")) // ���ȭ���� ��������
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x; // �ݸ����� box�ݶ��̴��� �ƴϱ� ������ ����ȯ �ڽ� �ݶ��̴��� ������Ʈ �ȿ� �־ ����
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount; // ��氹����ŭ ���ؼ� �ٽ� x�� �����ָ� �ǵڷ� ����
            collision.transform.position = pos;
            return;
        }

        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}
