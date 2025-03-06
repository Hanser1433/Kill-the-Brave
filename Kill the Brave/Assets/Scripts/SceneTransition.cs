using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Ŀ�곡���ͳ�����")]
    public string targetSceneName;      // �������ƣ�����Build Settings����ӣ�
    public Vector2 spawnPosition;       // ������³����ĳ�������

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ��ⴥ�������Ƿ������
        if (other.CompareTag("Player"))
        {
            // ��������㵽GameManager
            GameController.Instance.SetSpawnPoint(spawnPosition);
            // ����Ŀ�곡��
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
