using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
//Ѫ�������Ŀ��ƣ�����Ѫ������ʱ����
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image barImage; // Ѫ�����ͼ
    [SerializeField] private TextMeshProUGUI healthText; // Ѫ������
    [SerializeField] private CanvasGroup canvasGroup; // ������ʾ͸����
    private float maxHealth;
    private Coroutine hideCoroutine;

    private void Awake()
    {
        ShowInstant(false); // ��ʼ����
    }

    public void SetMaxHealth(float max)
    {
        maxHealth = max;
    }

    public void SetHealth(float current)
    {
        float fillAmount = Mathf.Clamp01(current / maxHealth);
        barImage.fillAmount = fillAmount;
        healthText.text = $"{Mathf.RoundToInt(current)} / {Mathf.RoundToInt(maxHealth)}";
        ShowTemporarily(); // ÿ�θ���ʱ��ʾ
    }

    private void ShowTemporarily()
    {
        ShowInstant(true);
        if (hideCoroutine != null)
            StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideAfterDelay(1.5f));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowInstant(false);
    }

    private void ShowInstant(bool show)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = show ? 1f : 0f;
            canvasGroup.blocksRaycasts = show;
        }
    }
}
