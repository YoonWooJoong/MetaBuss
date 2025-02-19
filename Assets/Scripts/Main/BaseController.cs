using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected StatHandler _statHandler;

    [SerializeField] private SpriteRenderer characterRenderer; // ĳ���� ������

    protected Vector2 movementDirection = Vector2.zero; // ������ ����

    protected AnimationHandler _animationHandler;
    public Vector2 MovementDirection { get { return movementDirection; } }


    protected virtual void Awake()
    {
        // ������Ʈ �ҷ�����
        _rigidbody = GetComponent<Rigidbody2D>();
        _statHandler = GetComponent<StatHandler>();
        _animationHandler = GetComponent<AnimationHandler>();
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection); // �̵� ���� player��Ʈ�ѷ����� �޾ƿ�d
    }

    private void Movement(Vector2 direction) // �̵����� �� �¿� �ٶ󺼽� flip
    {
        direction = direction * _statHandler.Speed;

        _rigidbody.velocity = direction;
        _animationHandler.Move(direction);

        if (characterRenderer != null) // ����ó��
        {
           //if (_rigidbody.velocity.x > 0)
           //    characterRenderer.flipX = false;
           //else if (_rigidbody.velocity.x < 0)
           //    characterRenderer.flipX = true;
        }
        else
        {
            Debug.LogError("BaseController�� characterRenderer�� null�Դϴ�.");
        }

        if (_rigidbody.velocity.x > 0)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        else if (_rigidbody.velocity.x < 0)
            transform.GetChild(0).rotation = Quaternion.Euler(0, 180, 0);


    }

}
