using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    // IsMove�� ���� int�� ��ȯ , ��Ʈ������ ���Ҷ����� �������� �ö󰣴�.

    [SerializeField] private Animator PlayerAnimator;
    [SerializeField] private Animator RideAnimator;
    private Animator NowAnimator;

    public void Start()
    {
        NowAnimator = PlayerAnimator;
    }

    public void Move(Vector2 obj)
    {
        NowAnimator.SetBool(IsMoving, obj.magnitude > .5f);

        // ��ü�� �����̰� �ִ��� �ƴ��� Ȯ���ϱ� ���� magnitude�� ũ�� Ȯ��
    }

    public void Die()
    {
        NowAnimator.SetBool(IsDie, true);
    }

    public void SwapAnimator(bool isActive)
    {
        if (isActive == true)
        {
            NowAnimator = PlayerAnimator;
        }
        else { NowAnimator = RideAnimator; }
    }
}
