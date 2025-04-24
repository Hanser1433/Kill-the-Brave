using UnityEngine;
using UnityEngine.UI;
//通过鼠标控制的操作，仅在玩家回合出现
public class BattleUIController : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [SerializeField] private Button healButton;
    [SerializeField] private Button skillsButton;
    [SerializeField] private Button runButton;
    [SerializeField] private BattleHandler battleHandler;

    private void Start()
    {
        attackButton.onClick.AddListener(OnAttackClicked);
        healButton.onClick.AddListener(OnHealClicked);
        ShowButtons(false);
    }

    private void OnAttackClicked()
    {
        ShowButtons(false);
        battleHandler.OnPlayerAction(0); // 0代表攻击
    }

    private void OnHealClicked()
    {
        ShowButtons(false);
        battleHandler.OnPlayerAction(1); // 1代表治疗
    }
    public void ShowButtons(bool isShow)
    {
        attackButton.gameObject.SetActive(isShow);
        healButton.gameObject.SetActive(isShow);
        skillsButton.gameObject.SetActive(isShow);
        runButton.gameObject.SetActive(isShow);
    }
}
