using UnityEngine;

[CreateAssetMenu(fileName = "HealSkill", menuName = "Skills/Heal")]
public class HealSkill : Skills
{
    public int healAmount;

    public override void Activate(CharacterBattle user, CharacterBattle target,System.Action onComplete)
    {
        if (user != null)
        {
            user.ReceiveHeal(healAmount);//Ϊʹ���߻ָ�Ѫ��
            onComplete?.Invoke();
            Debug.Log($"{user.name} ʹ�� {skillName} �ָ��� {healAmount} ��������");
        }
    }
}
