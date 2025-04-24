using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Button))]
public class ButtonTextColorHandler : MonoBehaviour
{
    private TMP_Text buttonText;
    private Button button;
    private Color normalColor;

    void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        normalColor = buttonText.color; // 保存初始颜色
    }

    // 按下时触发
    public void OnPointerDown()
    {
        buttonText.color = button.colors.pressedColor;
    }
    public void OnPointerSelect()
    {
        buttonText.color = button.colors.selectedColor;
    }
    public void OnPointerLeft()
    {
        buttonText.color = normalColor;
    }

    // 松开时触发
    public void OnPointerUp()
    {
        buttonText.color = normalColor;
    }
}
