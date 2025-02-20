using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        if(player !=null)
        {
            Vector3 PlayerPosition = new Vector3(player.position.x, player.position.y, transform.position.z); // 카메라의 z값을 가여와야해서 설정
            transform.position = Vector3.Lerp(transform.position, PlayerPosition, 2f*Time.deltaTime); // Lerp를 통한 살짝 느리게 따라가게함
        }
    }
}
