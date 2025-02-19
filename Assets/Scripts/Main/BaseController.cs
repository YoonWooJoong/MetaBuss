using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected StatHandler _statHandler;

    [SerializeField] private SpriteRenderer characterRenderer; // 캐릭터 렌더러

    protected Vector2 movementDirection = Vector2.zero; // 움직임 방향

    protected AnimationHandler _animationHandler;
    public Vector2 MovementDirection { get { return movementDirection; } }


    protected virtual void Awake()
    {
        // 컴포넌트 불러오기
        _rigidbody = GetComponent<Rigidbody2D>();
        _statHandler = GetComponent<StatHandler>();
        _animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection); // 이동 값은 player컨트롤러에서 받아옴d
    }

    private void Movement(Vector2 direction) // 이동로직 및 좌우 바라볼시 flip
    {
        direction = direction * _statHandler.Speed;

        _rigidbody.velocity = direction;
        _animationHandler.Move(direction);

        if (characterRenderer != null) // 예외처리
        {
           //if (_rigidbody.velocity.x > 0)
           //    characterRenderer.flipX = false;
           //else if (_rigidbody.velocity.x < 0)
           //    characterRenderer.flipX = true;
        }
        else
        {
            Debug.LogError("BaseController의 characterRenderer가 null입니다.");
        }

        if (_rigidbody.velocity.x > 0)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        else if (_rigidbody.velocity.x < 0)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);


    }

}
