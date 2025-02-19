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
            Debug.LogError("애니메이션핸들러를 찾을 수 없습니다.");
        if (_rigidbody2D == null)
            Debug.LogError("Rigidbody를 찾을 수 없습니다.");
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
            if (EventSystem.current.IsPointerOverGameObject()) // UI에 마우스가 올라가있을때 누르면 올라가지않게함
                return;
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                isFlap = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        Vector3 velocity = _rigidbody2D.velocity; // 캐릭터 이동을 위한 velocity
        velocity.x = forwardSpeed;

        if (isFlap) // 날고있는지 
        {
            velocity.y = flapForce;
            isFlap = false;
        }
        
        _rigidbody2D.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody2D.velocity.y * 10f), -90, 90); // z축을 움직여야 위아래로 움직임
        transform.rotation = Quaternion.Euler(0,0, angle); // 최대 180도만 움직이게 위로 90도 아래로 90도 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        isDead = true;
        deathCooldown = 1f;
        animationHandler.Die();    
    }
}
