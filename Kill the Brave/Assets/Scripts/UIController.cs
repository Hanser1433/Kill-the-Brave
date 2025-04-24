using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
[RequireComponent(typeof(Button))]
public class UIController : MonoBehaviour
{
    [Tooltip("������Ҫ��ת�ĳ������ƣ����ִ�Сд��")]
    public string targetSceneName; // ��Inspector�����õĳ�������
    private TMP_Text buttonText;
    private Button button;
    private Color normalColor;
    void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        normalColor = buttonText.color; // �����ʼ��ɫ
    }

    // ����ʱ����
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

    // �ɿ�ʱ����
    public void OnPointerUp()
    {
        buttonText.color = normalColor;
    }
}
