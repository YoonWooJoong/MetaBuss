using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 20)][SerializeField] private float speed = 3f;
    public float Speed // 캐릭터 스피드
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
