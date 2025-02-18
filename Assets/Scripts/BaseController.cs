using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }


    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
    }

    private void Movement(Vector2 direction)
    {
        direction = direction * 5f;

        _rigidbody.velocity = direction;

        if (characterRenderer != null)
        {
            if (_rigidbody.velocity.x > 0)
                characterRenderer.flipX = false;
            else if (_rigidbody.velocity.x < 0)
                characterRenderer.flipX = true;
        }
        else
        {
            Debug.LogError("BaseController의 characterRenderer가 null입니다.");
        }

    }

}
