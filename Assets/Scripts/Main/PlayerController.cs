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
        Ride = inputActions.FindAction("Ride");
        Ride.performed += OnZKeyPressed;
        Ride.Enable();
    }


    private void OnZKeyPressed(InputAction.CallbackContext context)
    {
        isActive = !isActive;
        animationHandler.SwapAnimator(isActive);
        RideObject.transform.gameObject.SetActive(!isActive);
        if (isActive == false)
        {
            _statHandler.Speed = 6f;
            PlayerBoxCollider.offset = new Vector2(0, -0.3f);
            PlayerBoxCollider.size = new Vector2(PlayerBoxCollider.size.x, PlayerBoxCollider.size.y * 1.5f);
        }
        else 
        {
            _statHandler.Speed = 3f;
            PlayerBoxCollider.offset = new Vector2(0, 0);
            PlayerBoxCollider.size = new Vector2(PlayerBoxCollider.size.x, PlayerBoxCollider.size.y / 1.5f);
        }
        if (rendereranim == null)
        {
            Debug.Log("animtor를 찾을수 없습니다.");
        }
        else { rendereranim.enabled = isActive; }
    }
    void OnMove(InputValue inputValue) // 인풋 시스템
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

}
