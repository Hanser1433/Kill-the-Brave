
//角色表现层
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBattle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;//动画的移动速度
    [SerializeField] private GameObject turnIndicator;//指示器
    [SerializeField] private HealthBar healthBar;//血条
    [SerializeField] private GameObject pfDamagePopup;//伤害飘字
    [SerializeField] private List<Skills> skillList;
    public List<Skills> GetSkillList() => skillList;


    public CharacterBase characterBase;
    private Animator animator;
    private Vector3 startPosition;

    private void Awake()//初始化：1.获取绑定2.设置初始位置3.初始化血条ui
    {
        characterBase = GetComponent<CharacterBase>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        startPosition = transform.position;
        healthBar.SetMaxHealth(characterBase.GetMaxHealth());
        //healthBar.SetHealth(characterBase.GetCurrentHealth());
    }

    public void SetTurnIndicator(bool isActive)//设置指示器
    {
        if (turnIndicator != null) turnIndicator.SetActive(isActive);
    }

    public void PlayAttack(CharacterBattle target, System.Action onHit, System.Action onComplete)//传入onhit和oncomplete两个函数，在需要的地方调用
    {
        StartCoroutine(DoAttack(target, onHit, onComplete));//当协程全部逻辑完成以后返回
    }

    private IEnumerator DoAttack(CharacterBattle target, System.Action onHit, System.Action onComplete)//传入onhit和oncomplete两个函数，在需要的地方调用
    {
        // 移动到敌人前面
        Vector3 attackPos = target.transform.position + (transform.position.x < target.transform.position.x ? Vector3.left : Vector3.right) * 1.5f;
        yield return MoveTo(attackPos);
        // 播放攻击动画
        animator.SetBool("IsAttaching", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("IsAttaching", false);

        // 调用 onHit：这里进行伤害计算
        onHit?.Invoke();//调用传进来的onhit函数

        // 回到原位置
        yield return MoveTo(startPosition);

        // 角色动作完成
        onComplete?.Invoke();//调用传进来的oncomplete函数，即onActionComplete
    }



    private IEnumerator MoveTo(Vector3 destination)
    {
        animator.SetBool("IsRunning", true);
        while (Vector3.Distance(transform.position, destination) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
        animator.SetBool("IsRunning", false);
    }

    /*public void ReceiveDamage(int damage)//受伤
    {
        characterBase.TakeDamage(damage);//数据
        healthBar.SetHealth(characterBase.GetCurrentHealth());//血条
        ShowDamagePopup(damage);//飘字
    }
    public void ReceiveHeal(int healAmount)//治疗
    {
        characterBase.Heal(healAmount);
        healthBar.SetHealth(characterBase.GetCurrentHealth());//血条
        ShowHealPopup(healAmount);//飘字
    }*/
    public void ReceiveDamage(int damage)
    {
        characterBase.TakeDamage(damage);
        healthBar.SetHealth(characterBase.GetCurrentHealth());
        ShowDamagePopup(damage);
    }

    public void ReceiveHeal(int healAmount)
    {
        characterBase.Heal(healAmount);
        healthBar.SetHealth(characterBase.GetCurrentHealth());
        ShowHealPopup(healAmount);
    }


    private void ShowDamagePopup(int damage)
    {
        if (pfDamagePopup == null) return;
        var popup = Instantiate(pfDamagePopup, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        popup.GetComponent<FloatingDamageText>().Setup("-" + damage, Color.red);
    }
    private void ShowHealPopup(int healAmount)
    {
        if (pfDamagePopup == null) return;
        var popup = Instantiate(pfDamagePopup, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        popup.GetComponent<FloatingDamageText>().Setup("+" + healAmount, Color.green);
    }

    public bool IsDead() => characterBase.IsDead();
}
