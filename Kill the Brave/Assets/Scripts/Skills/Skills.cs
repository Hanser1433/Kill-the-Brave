using UnityEngine;

public abstract class Skills : ScriptableObject
{
    public string skillName;
    public Sprite icon;//技能图标
    public int manaCost;//魔耗

    // 技能执行的通用接口，该方法必须由子类具体实现
    public abstract void Activate(CharacterBattle user, CharacterBattle target, System.Action onComplete);
}
