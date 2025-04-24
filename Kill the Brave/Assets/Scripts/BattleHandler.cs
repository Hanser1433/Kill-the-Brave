
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

    private void Start()//初始化：1.生成角色实例2.生成指示器3.UI
    {
        //建立预制体时会触发其上面所有组件的awake
        //初始化玩家队伍
        for(int i = 0; i < pfPlayerChracters.Count; i++)
        {
            var a = Instantiate(pfPlayerChracters[i], playerTeamPosition.position + new Vector3(i * 2, i * -2), Quaternion.identity);
            var b=a.GetComponent<CharacterBattle>();
            playerTeam.Add(b);
            allBattlers.Add(b);
            //信息面板
            staticCharacterPanels[i].SetCharacter(b);
        }
        for (int i = 0; i < pfEnemyChracters.Count; i++)
        {
            var a = Instantiate(pfEnemyChracters[i], enemyTeamPosition.position + new Vector3(i * 2, i * 2), Quaternion.identity);
            var b = a.GetComponent<CharacterBattle>();
            enemyTeam.Add(b);
            allBattlers.Add(b);
        }
        //以前的
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
            battleUIController.ShowButtons(true);//玩家回合，开启技能选择UI
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            canInput = false;
            if (isPlayer)
            {
                current.SetTurnIndicator(false);//已经做出决策，取消光标
                battleUIController.ShowButtons(false);//取消技能选择UI
                current.GetSkillList()[0].Activate(current, GetFirstAlive(enemyTeam), OnActionComplete);
            }
            else
            {
                current.SetTurnIndicator(false);//已经做出决策，取消光标
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
    public void OnPlayerAction(int actionType)//通过鼠标的操作
    {
        CharacterBattle current = allBattlers[turnIndex];
        if (isGameOver || !canInput || !playerTeam.Contains(current)) return;//只有玩家可选择
        bool isPlayer = playerTeam.Contains(current);
        canInput = false;
        current.SetTurnIndicator(false);
        if (actionType == 0) // 攻击
        {
            current.GetSkillList()[0].Activate(current, GetFirstAlive(enemyTeam), OnActionComplete);
        }
        else if (actionType == 1) // 治疗
        {
            current.GetSkillList()[1].Activate(current, GetFirstAlive(enemyTeam), OnActionComplete);
        }
    }

    private void OnActionComplete()//执行完全部技能后回到此处
    {
        //判断胜负
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
        // 找到下一个还活着的角色
        do
        {
            turnIndex = (turnIndex + 1) % allBattlers.Count;
        } while (allBattlers[turnIndex].IsDead());
        UpdateTurnIndicator();//切换指示器
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
        gameOverText.text = playerWin ? "你赢了!" : "你输了!";
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
