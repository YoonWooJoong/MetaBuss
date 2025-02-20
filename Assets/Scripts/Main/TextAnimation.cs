using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private static readonly int IsHurry = Animator.StringToHash("IsHurry");
    [SerializeField] private Animator TexTAnimator;


    public void Hurry() // 시간 텍스트 에니메이션 20초 남았을때 활성화
    {
        TexTAnimator.SetBool(IsHurry, true);
    }

    public void NoHurry() // 평소 상태
    {
        TexTAnimator.SetBool(IsHurry, false);
    }
}
