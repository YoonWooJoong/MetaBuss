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
        animator = GetComponentInChildren<Animator>();
        colorDropDown.options.Clear();

        colorDropDown.options.Add(new TMP_Dropdown.OptionData("Pumkin"));
        colorDropDown.options.Add(new TMP_Dropdown.OptionData("Knight"));
        colorDropDown.options.Add(new TMP_Dropdown.OptionData("Skeleton"));

        colorDropDown.value = 0;
        colorDropDown.RefreshShownValue();

        colorDropDown.onValueChanged.AddListener(ChangeCharactor);
    }


    private void ChangeCharactor(int value)
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
