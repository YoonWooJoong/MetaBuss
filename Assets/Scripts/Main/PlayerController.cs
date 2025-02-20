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
        Ride = inputActions.FindAction("Ride"); // ���̵� Action�� ã�Ƽ� Ride�� �־���
        Ride.performed += OnZKeyPressed; // �̺�Ʈ �Ҵ�
        Ride.Enable();
    }


    private void OnZKeyPressed(InputAction.CallbackContext context)
    {
        isActive = !isActive; // ����������� ����� ���� �̷��� «
        animationHandler.SwapAnimator(isActive); // ���ϸ��̼� �ڵ鷯�� NowAnimator�� ���ϱ����� �޼���
        RideObject.transform.gameObject.SetActive(!isActive); // isActive�� false���� ���̵� Ȱ��ȭ
        if (isActive == false) // ���̵带 ������ �ݶ��̴��� ���ǵ� ����
        {
            _statHandler.Speed = 6f;
            PlayerBoxCollider.offset = new Vector2(0, -0.3f);
            PlayerBoxCollider.size = new Vector2(PlayerBoxCollider.size.x, PlayerBoxCollider.size.y * 1.5f);
        }
        else // ���̵带 Ÿ�� ������
        {
            _statHandler.Speed = 3f;
            PlayerBoxCollider.offset = new Vector2(0, 0);
            PlayerBoxCollider.size = new Vector2(PlayerBoxCollider.size.x, PlayerBoxCollider.size.y / 1.5f);
        }
        if (rendereranim == null)
        {
            Debug.Log("animtor�� ã���� �����ϴ�.");
        }
        else { rendereranim.enabled = isActive; } // ĳ���� ���ν�������Ʈ�� animator�� �ν�����â���� Ȱ�� ��Ȱ��
    }
    void OnMove(InputValue inputValue) // ��ǲ �ý���
    {
        movementDirection = inputValue.Get<Vector2>();
        movementDirection = movementDirection.normalized;
    }

}
