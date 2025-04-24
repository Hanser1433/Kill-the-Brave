
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int attackPower = 10;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    //接口
    public int GetAttackPower() => attackPower;
    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;

    public void TakeDamage(int damage)//受伤
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);//避免血量为负数
    }
    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
    }
    public bool IsDead()//死亡返回true
    {
        return currentHealth <= 0;
    }
}
