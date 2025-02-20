using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miro : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) // 미로벽에 플레이어가 부딫힐시 원점으로 돌아감
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = new Vector3(-58.45f, 7.45f, 0);
        }
    }
}
