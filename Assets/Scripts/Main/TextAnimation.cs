using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    private static readonly int IsHurry = Animator.StringToHash("IsHurry");
    [SerializeField] private Animator TexTAnimator;


    public void Hurry()
    {
        TexTAnimator.SetBool(IsHurry, true);
    }

    public void NoHurry()
    {
        TexTAnimator.SetBool(IsHurry, false);
    }
}
