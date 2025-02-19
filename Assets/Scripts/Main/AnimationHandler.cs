using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    // IsMove의 값을 int로 변환 , 스트링끼리 비교할때보다 안전성이 올라간다.

    protected Animator Animator;


    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>(); 
        // player 자식으로 mainSprite가 있기에 자식에서 컴포넌트를 찾기위해 사용
    }

    public void Move(Vector2 obj)
    {
        Animator.SetBool(IsMoving, obj.magnitude > .5f); 
        // 물체가 움직이고 있는지 아닌지 확인하기 위해 magnitude로 크기 확인
    }

    public void Die()
    {
        Animator.SetBool(IsDie, true);
    }
}
