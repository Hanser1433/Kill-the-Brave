using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { FreeRoam,Dialog,Battle}
public class GameController : MonoBehaviour
{
    GameState state;
    //GameController为全局唯一访问点，管理全局数据，因此使用实例
    public static GameController Instance;
    private Vector2 _spawnPosition;
    public PlayerController PlayerController;
    void Awake()
    {
        //确保Instance唯一
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//保护gameController不被删除
            SceneManager.sceneLoaded += OnSceneLoaded;//订阅场景加载事件,当 sceneLoaded 事件触发时，Unity 会调用所有订阅此事件的方法（如 OnSceneLoaded）
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//在加载新场景以后触发
    {
        //// 确保玩家引用有效
        //if (PlayerController == null)
        //{
        //    // 仅在极端情况下生成新玩家（如首次启动）
        //    PlayerController = FindObjectOfType<PlayerController>();
        //    if (PlayerController == null && playerPrefab != null)
        //    {
        //        GameObject player = Instantiate(playerPrefab, _spawnPosition, Quaternion.identity);
        //        PlayerController = player.GetComponent<PlayerController>();
        //    }
        //}
        // 更新玩家位置
        if (PlayerController != null)
        {
            PlayerController.transform.position = _spawnPosition;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void SetSpawnPoint(Vector2 pos)
    {
        _spawnPosition = pos;
    }
    public Vector2 GetSpawnPoint()
    {
        return _spawnPosition;
    }
    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if(state==GameState.Dialog)
                state = GameState.FreeRoam;
        };

    }
    private void Update()
    {
        if (PlayerController == null) return; // 避免空引用
        if (state == GameState.FreeRoam)
        {
            PlayerController.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {

        }
    }
}
