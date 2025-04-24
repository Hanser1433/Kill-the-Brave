using UnityEngine;
using UnityEngine.UI;
//ͨ�������ƵĲ�����������һغϳ���
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
        battleHandler.OnPlayerAction(0); // 0������
    }

    private void OnHealClicked()
    {
        ShowButtons(false);
        battleHandler.OnPlayerAction(1); // 1��������
    }
    public void ShowButtons(bool isShow)
    {
        attackButton.gameObject.SetActive(isShow);
        healButton.gameObject.SetActive(isShow);
        skillsButton.gameObject.SetActive(isShow);
        runButton.gameObject.SetActive(isShow);
    }
}
