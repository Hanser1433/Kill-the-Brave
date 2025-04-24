using UnityEngine;

public abstract class Skills : ScriptableObject
{
    public string skillName;
    public Sprite icon;//����ͼ��
    public int manaCost;//ħ��

    // ����ִ�е�ͨ�ýӿڣ��÷����������������ʵ��
    public abstract void Activate(CharacterBattle user, CharacterBattle target, System.Action onComplete);
}
