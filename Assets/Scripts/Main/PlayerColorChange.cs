using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorChange : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown colorDropDown;
    protected SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        colorDropDown.options.Clear();

        colorDropDown.options.Add(new TMP_Dropdown.OptionData("base"));
        colorDropDown.options.Add(new TMP_Dropdown.OptionData("blue"));
        colorDropDown.options.Add(new TMP_Dropdown.OptionData("green"));
        colorDropDown.options.Add(new TMP_Dropdown.OptionData("red"));

        colorDropDown.value = 0;
        colorDropDown.RefreshShownValue();

        colorDropDown.onValueChanged.AddListener(ChangeColor);
    }

    private void ChangeColor(int value) // 드롭다운으로 만든 색상 변경
    {
        switch (value)
        {
            case 0:
                spriteRenderer.color = Color.white;
                break;
            case 1:
                spriteRenderer.color = Color.blue;
                break;
            case 2:
                spriteRenderer.color = Color.green;
                break;
            case 3:
                spriteRenderer.color = Color.red;
                break;
        }
    }

}
