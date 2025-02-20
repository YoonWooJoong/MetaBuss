using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    bool isActive = true;
    [SerializeField] private Animator rendereranim;
    [SerializeField] private Transform RideObject;
    [SerializeField] private BoxCollider2D PlayerBoxCollider;
    public InputActionAsset inputActions;
    private InputAction Ride;
    private AnimationHandler animationHandler;




    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }



    public void Start()
    {
        animationHandler = GetComponent<AnimationHandler>();
        Ride = inputActions.FindAction("Ride"); // 라이드 Action을 찾아서 Ride에 넣어줌
        Ride.performed += OnZKeyPressed; // 이벤트 할당
        Ride.Enable();
    }


    private void OnZKeyPressed(InputAction.CallbackContext context)
    {
        isActive = !isActive; // 토글형식으로 만들기 위해 이렇게 짬
        animationHandler.SwapAnimator(isActive); // 에니메이션 핸들러의 NowAnimator를 정하기위한 메서드
        RideObject.transform.gameObject.SetActive(!isActive); // isActive가 false여야 라이드 활성화
        if (isActive == false) // 라이드를 탔을때 콜라이더와 스피드 조정
        {
            _statHandler.Speed = 6f;
            PlayerBoxCollider.offset = new Vector2(0, -0.3f);
            PlayerBoxCollider.size = new Vector2(PlayerBoxCollider.size.x, PlayerBoxCollider.size.y * 1.5f);
        }
        else // 라이드를 타지 않을때
        {
            _statHandler.Speed = 3f;
            PlayerBoxCollider.offset = new Vector2(0, 0);
            PlayerBoxCollider.size = new Vector2(PlayerBoxCollider.size.x, PlayerBoxCollider.size.y / 1.5f);
        }
        if (rendereranim == null)
        {
            Debug.Log("animtor를 찾을수 없습니다.");
        }
        else { rendereranim.enabled = isActive; } // 캐릭터 메인스프라이트의 animator를 인스펙터창에서 활성 비활성
    }
    void OnMove(InputValue inputValue) // 인풋 시스템
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

}
