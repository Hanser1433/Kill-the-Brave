
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> pfPlayerChracters;
    [SerializeField] private List<GameObject> pfEnemyChracters;
    [SerializeField] private Transform playerTeamPosition;
    [SerializeField] private Transform enemyTeamPosition;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private BattleUIController battleUIController;
    private List<CharacterBattle> playerTeam=new List<CharacterBattle>();
    private List<CharacterBattle> enemyTeam=new List<CharacterBattle>();
    private List<CharacterBattle> allBattlers = new List<CharacterBattle>();
    public List<CharacterInfoPanel> staticCharacterPanels;


    private int turnIndex = 0;
    private bool canInput = true;
    private bool isGameOver = false;

    private void Start()//��ʼ����1.���ɽ�ɫʵ��2.����ָʾ��3.UI
    {
        //����Ԥ����ʱ�ᴥ�����������������awake
        //��ʼ����Ҷ���
        for(int i = 0; i < pfPlayerChracters.Count; i++)
        {
            var a = Instantiate(pfPlayerChracters[i], playerTeamPosition.position + new Vector3(i * 2, i * -2), Quaternion.identity);
            var b=a.GetComponent<CharacterBattle>();
            playerTeam.Add(b);
            allBattlers.Add(b);
            //��Ϣ���
            staticCharacterPanels[i].SetCharacter(b);
        }
        for (int i = 0; i < pfEnemyChracters.Count; i++)
        {
            var a = Instantiate(pfEnemyChracters[i], enemyTeamPosition.position + new Vector3(i * 2, i * 2), Quaternion.identity);
            var b = a.GetComponent<CharacterBattle>();
            enemyTeam.Add(b);
            allBattlers.Add(b);
        }
        //��ǰ��
        UpdateTurnIndicator();
        gameOverText.text = "";
        battleUIController.ShowButtons(false);
    }

    private void Update()
    {
        if (!canInput || isGameOver) return;
        CharacterBattle current = allBattlers[turnIndex];
        bool isPlayer=playerTeam.Contains(current);
        if (isPlayer)
        {
            battleUIController.ShowButtons(true);//��һغϣ���������ѡ��UI
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            canInput = false;
            if (isPlayer)
            {
                current.SetTurnIndicator(false);//�Ѿ��������ߣ�ȡ�����
                battleUIController.ShowButtons(false);//ȡ������ѡ��UI
                current.GetSkillList()[0].Activate(current, GetFirstAlive(enemyTeam), OnActionComplete);
            }
            else
            {
                current.SetTurnIndicator(false);//�Ѿ��������ߣ�ȡ�����
                current.GetSkillList()[0].Activate(current, GetFirstAlive(playerTeam), OnActionComplete); ;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            canInput = false;
            if (isPlayer)
            {
                current.SetTurnIndicator(false);
                battleUIController.ShowButtons(false);
                current.GetSkillList()[1].Activate(current, GetFirstAlive(enemyTeam), OnActionComplete);
            }
            else
            {   current.SetTurnIndicator(false);
                current.GetSkillList()[1].Activate(current, GetFirstAlive(playerTeam), OnActionComplete);
            }
        }
    }
    public void OnPlayerAction(int actionType)//ͨ�����Ĳ���
    {
        CharacterBattle current = allBattlers[turnIndex];
        if (isGameOver || !canInput || !playerTeam.Contains(current)) return;//ֻ����ҿ�ѡ��
        bool isPlayer = playerTeam.Contains(current);
        canInput = false;
        current.SetTurnIndicator(false);
        if (actionType == 0) // ����
        {
            current.GetSkillList()[0].Activate(current, GetFirstAlive(enemyTeam), OnActionComplete);
        }
        else if (actionType == 1) // ����
        {
            current.GetSkillList()[1].Activate(current, GetFirstAlive(enemyTeam), OnActionComplete);
        }
    }

    private void OnActionComplete()//ִ����ȫ�����ܺ�ص��˴�
    {
        //�ж�ʤ��
        if (IsTeamDead(playerTeam))
        {
            EndGame(false);
            return;
        }
        if (IsTeamDead(enemyTeam))
        {
            EndGame(true);
            return;
        }
        // �ҵ���һ�������ŵĽ�ɫ
        do
        {
            turnIndex = (turnIndex + 1) % allBattlers.Count;
        } while (allBattlers[turnIndex].IsDead());
        UpdateTurnIndicator();//�л�ָʾ��
        canInput = true;
    }

    private void UpdateTurnIndicator()
    {
        for (int i = 0; i < allBattlers.Count; i++)
        {
            if (i == turnIndex)
                allBattlers[i].SetTurnIndicator(true);
            else
                allBattlers[i].SetTurnIndicator(false);
        }
    }

    private void EndGame(bool playerWin)
    {
        isGameOver = true;
        gameOverText.text = playerWin ? "��Ӯ��!" : "������!";
        gameOverText.gameObject.SetActive(true);
    }
    private bool IsTeamDead(List<CharacterBattle> team)
    {
        for(int i = 0;i < team.Count;i++)
        {
            if (!team[i].IsDead())
                return false;
        }
        return true;
    }
    private CharacterBattle GetFirstAlive(List<CharacterBattle> team)
    {
        for(int i=0;i<team.Count;i++)
        {
            if (!team[i].IsDead())
            {
                return team[i];
            }
        }
        return null;
    }
}
