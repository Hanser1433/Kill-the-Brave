using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
[RequireComponent(typeof(Button))]
public class UIController : MonoBehaviour
{
    [Tooltip("输入需要跳转的场景名称（区分大小写）")]
    public string targetSceneName; // 在Inspector中配置的场景名称
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
        SceneManager.LoadScene(targetSceneName);
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
