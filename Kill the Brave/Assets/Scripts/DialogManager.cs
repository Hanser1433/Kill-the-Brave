using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogbox;//对话框
    [SerializeField] GameObject dialogboxSpeaker;//显示说话人的对话框
    [SerializeField] Text dialogText;//文本
    [SerializeField] Text speakerText;//说话人姓名
    [SerializeField] int lettersPerSecond;//每秒出现字数
    public event Action OnShowDialog;//事件：展示对话
    public event Action OnHideDialog;//事件：结束对话
    public static DialogManager Instance { get; private set; }//单例模式，确保dialogmanager在项目中只有一个实例
    Dialog dialog;
    Dialog speaker;
    int currentLine = 0;
    bool isTyping;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//保护DialogManager不被删除
        }
       
    }
    public IEnumerator ShowDialog(Dialog dialog,Dialog speaker)
    {
        yield return new WaitForEndOfFrame();//强制协程在当前帧结束后再继续执行后续逻辑。
        OnShowDialog?.Invoke();//用于​触发事件。它的本质是 ​将函数的调用“广播”给所有订阅该事件的监听者。
        this.dialog = dialog;//把npc传入函数的dialog设置为当前对象的dialog
        this.speaker = speaker;
        dialogbox.SetActive(true);//激活dialogbox
        dialogboxSpeaker.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0], speaker.Lines[0]));//启动逐字显示对话的协程
    }
    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ++currentLine;
            if (currentLine < dialog.Lines.Count)//避免越界
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine], speaker.Lines[currentLine]));//开始逐行打字
            }
            else//达到终止条件
            {
                dialogbox.SetActive(false);//关闭dialogbox
                dialogboxSpeaker.SetActive(false);
                currentLine = 0;//回到起点
                OnHideDialog?.Invoke();//触发onhidedialog事件
            }
        }
    }
    //// ​让对话框中的文字像打字机一样逐字出现
    public IEnumerator TypeDialog(string line,string speaker)
    {
        isTyping = true;
        speakerText.text = speaker;
        dialogText.text = "";//清空
        foreach (var letter in line.ToCharArray())//ToCharArray()：将字符串转换为字符数组,遍历字符串的字符
        {
            dialogText.text += letter;//将字符依次添加到文本框
            yield return new WaitForSeconds(1f / lettersPerSecond);//控制打字时间间隔为1f / lettersPerSecond
        }
        isTyping = false;
    }
}
