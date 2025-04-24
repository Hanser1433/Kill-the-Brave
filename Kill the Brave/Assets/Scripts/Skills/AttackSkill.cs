using UnityEngine;

[CreateAssetMenu(fileName = "AttackSkill", menuName = "Skills/Attack")]//在 Unity 编辑器中提供一个菜单项，用于快速创建对应类型的 ScriptableObject 实例。
public class AttackSkill : Skills
{

    public override void Activate(CharacterBattle user, CharacterBattle target, System.Action onComplete)//传入了函数onActioncomplete作为参数
    {
        if (target != null)
        {
            int damage = CalculateDamage(user, target);
            //这一步相当于传进三个参数，中间是lambda表达式，其本质是一个函数
            user.PlayAttack(target,
                () => {
                    target.ReceiveDamage(damage);
                    Debug.Log($"{user.name} 对 {target.name} 造成 {damage} 点伤害！");
                },
                onComplete//调用onActioncomplete（）函数
            );
        }
    }

    private int CalculateDamage(CharacterBattle user, CharacterBattle target)
    {
        // 未来可以在这里写不同公式，比如：暴击、技能倍率等等
        return user.characterBase.GetAttackPower();
    }



}
