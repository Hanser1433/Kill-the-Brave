using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterInfoPanel : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI nameText;
    //[SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image healthFill;
    [SerializeField] private TextMeshProUGUI healthText;

    private CharacterBattle targetCharacter;

    public void SetCharacter(CharacterBattle character)
    {
        targetCharacter = character;
        //nameText.text = character.name; // 或自定义
        //levelText.text = "Lv1"; // 后续可扩展
        UpdateHealth(); // 初始化
    }

    private void Update()
    {
        if (targetCharacter != null)
        {
            UpdateHealth();
        }
    }

    private void UpdateHealth()
    {
        float cur = targetCharacter.characterBase.GetCurrentHealth();
        float max = targetCharacter.characterBase.GetMaxHealth();
        healthFill.fillAmount = Mathf.Clamp01(cur / max);
        healthText.text = $"{cur} / {max}";
    }
}
