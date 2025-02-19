using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDie = Animator.StringToHash("IsDie");
    // IsMove�� ���� int�� ��ȯ , ��Ʈ������ ���Ҷ����� �������� �ö󰣴�.

    protected Animator Animator;


    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>(); 
        // player �ڽ����� mainSprite�� �ֱ⿡ �ڽĿ��� ������Ʈ�� ã������ ���
    }

    public void Move(Vector2 obj)
    {
        Animator.SetBool(IsMoving, obj.magnitude > .5f); 
        // ��ü�� �����̰� �ִ��� �ƴ��� Ȯ���ϱ� ���� magnitude�� ũ�� Ȯ��
    }

    public void Die()
    {
        Animator.SetBool(IsDie, true);
    }
}
