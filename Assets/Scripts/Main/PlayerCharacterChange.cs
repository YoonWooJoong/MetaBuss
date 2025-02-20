using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCharacterChange : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown colorDropDown;
    protected Animator animator;

    private void Start()
    {
        //드롭다운으로 캐릭터 3가지 변경
        animator = GetComponentInChildren<Animator>();
        colorDropDown.options.Clear();

        colorDropDown.options.Add(new TMP_Dropdown.OptionData("Pumkin"));
        colorDropDown.options.Add(new TMP_Dropdown.OptionData("Knight"));
        colorDropDown.options.Add(new TMP_Dropdown.OptionData("Skeleton"));

        colorDropDown.value = 0;
        colorDropDown.RefreshShownValue();

        colorDropDown.onValueChanged.AddListener(ChangeCharactor);
    }


    private void ChangeCharactor(int value) // 리소스로드를 통해 리소스에 애니메이터를 넣어서 가져옴
    {
        switch (value)
        {
            case 0:
                animator.runtimeAnimatorController = Resources.Load("Pumpkin") as RuntimeAnimatorController;
                break;
            case 1:
                animator.runtimeAnimatorController = Resources.Load("Knight") as RuntimeAnimatorController;
                break;
            case 2:
                animator.runtimeAnimatorController = Resources.Load("Skeleton") as RuntimeAnimatorController;
                break;
        }
    }

}
