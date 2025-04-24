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
        //nameText.text = character.name; // ���Զ���
        //levelText.text = "Lv1"; // ��������չ
        UpdateHealth(); // ��ʼ��
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
