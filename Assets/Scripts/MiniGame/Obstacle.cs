using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // 방해물이 전체적으로 위로 나올것인지 아래로 나올것인지 위치 조정
    private float highPosY = 1f; 
    private float lowPosY = -1f;
    // 방해물의 구멍사이즈
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
        float holeSize = Random.Range(holeSizeMin, holeSizeMax); // 구멍 사이즈를 랜덤으로 구함
        float halfHoleSize = holeSize / 2; // 구한 값을 반으로 나눔

        topObject.localPosition = new Vector3(0, halfHoleSize); // 반으로 나눈것을 위에서는 로컬포지션으로 위로올림
        bottomObject.localPosition = new Vector3(0, -halfHoleSize); // 아래로 내림

        // 새로 생성된 것이기 때문에 마지막 생성물에 widthPadding을 더해서 생성해줌
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY); // 전체적으로 위아래 나오는 생성위치를 랜덤으로 구해줌

        transform.position = placePosition; // 생성물의 포지션에 구한 포지션 값을 넣어줌

        return placePosition; // 위치 반환
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
