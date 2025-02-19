using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    // IsMove의 값을 int로 변환 , 스트링끼리 비교할때보다 안전성이 올라간다.

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

        // 물체가 움직이고 있는지 아닌지 확인하기 위해 magnitude로 크기 확인
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
