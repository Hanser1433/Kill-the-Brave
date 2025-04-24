using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
//血条动画的控制，仅在血量更新时浮现
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image barImage; // 血条填充图
    [SerializeField] private TextMeshProUGUI healthText; // 血量文字
    [SerializeField] private CanvasGroup canvasGroup; // 控制显示透明度
    private float maxHealth;
    private Coroutine hideCoroutine;

    private void Awake()
    {
        ShowInstant(false); // 初始隐藏
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
        ShowTemporarily(); // 每次更新时显示
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
