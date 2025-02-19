using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    private int numBgCount = 5; // 배경 카운트
    private int obstacleCount = 0; // 방해물 카운트
    private Vector3 obstacleLastPosition = Vector3.zero; // 방해물의 마지막 위치

    // Start is called before the first frame update
    void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>(); // 방해물 오브젝트를 찾고 배열에 넣음
        obstacleLastPosition = obstacles[0].transform.position; // 맨 첫번째 방해물의 우치를 마지막위치로 넣음
        obstacleCount = obstacles.Length; // 방해물의 갯수만큼을 카운트에 넣음

        for (int i = 0; i < obstacleCount; i++) // 카운트만큼 반복해서 방해물 위치를 모두 설정해줌
        {
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BackGround")) // 배경화면을 만났을때
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x; // 콜리전은 box콜라이더가 아니기 때문에 형변환 박스 콜라이더가 오브젝트 안에 있어서 가능
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount; // 배경갯수만큼 곱해서 다시 x에 더해주면 맨뒤로 보냄
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
