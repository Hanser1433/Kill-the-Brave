using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("目标场景和出生点")]
    public string targetSceneName;      // 场景名称（需在Build Settings中添加）
    public Vector2 spawnPosition;       // 玩家在新场景的出生坐标

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检测触发对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 保存出生点到GameManager
            GameController.Instance.SetSpawnPoint(spawnPosition);
            // 加载目标场景
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
