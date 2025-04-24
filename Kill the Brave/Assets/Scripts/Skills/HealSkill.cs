using UnityEngine;

[CreateAssetMenu(fileName = "HealSkill", menuName = "Skills/Heal")]
public class HealSkill : Skills
{
    public int healAmount;

    public override void Activate(CharacterBattle user, CharacterBattle target,System.Action onComplete)
    {
        if (user != null)
        {
            user.ReceiveHeal(healAmount);//为使用者恢复血量
            onComplete?.Invoke();
            Debug.Log($"{user.name} 使用 {skillName} 恢复了 {healAmount} 点生命！");
        }
    }
}
