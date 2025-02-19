using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGamePlayer : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    protected AnimationHandler animationHandler;
    private GameManager gameManager;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isFlap = false;

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    // Start is called before the first frame update
    void Start()
    {
        animationHandler = GetComponent<AnimationHandler>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (animationHandler == null)
            Debug.LogError("�ִϸ��̼��ڵ鷯�� ã�� �� �����ϴ�.");
        if (_rigidbody2D == null)
            Debug.LogError("Rigidbody�� ã�� �� �����ϴ�.");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                gameManager.MiniGameOver();   
            }
            else { deathCooldown -= Time.deltaTime; }
        }
        else 
        {
            if (EventSystem.current.IsPointerOverGameObject()) // UI�� ���콺�� �ö������� ������ �ö����ʰ���
                return;
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                isFlap = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody2D.velocity; // ĳ���� �̵��� ���� velocity
        velocity.x = forwardSpeed;

        if (isFlap) // �����ִ��� 
        {
            velocity.y = flapForce;
            isFlap = false;
        }
        
        _rigidbody2D.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody2D.velocity.y * 10f), -90, 90); // z���� �������� ���Ʒ��� ������
        transform.rotation = Quaternion.Euler(0,0, angle); // �ִ� 180���� �����̰� ���� 90�� �Ʒ��� 90�� 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        isDead = true;
        deathCooldown = 1f;
        animationHandler.Die();    
    }
}
