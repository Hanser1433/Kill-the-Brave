using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { FreeRoam,Dialog,Battle}
public class GameController : MonoBehaviour
{
    GameState state;
    //GameControllerΪȫ��Ψһ���ʵ㣬����ȫ�����ݣ����ʹ��ʵ��
    public static GameController Instance;
    private Vector2 _spawnPosition;
    public PlayerController PlayerController;
    void Awake()
    {
        //ȷ��InstanceΨһ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);//����gameController����ɾ��
            SceneManager.sceneLoaded += OnSceneLoaded;//���ĳ��������¼�,�� sceneLoaded �¼�����ʱ��Unity ��������ж��Ĵ��¼��ķ������� OnSceneLoaded��
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//�ڼ����³����Ժ󴥷�
    {
        //// ȷ�����������Ч
        //if (PlayerController == null)
        //{
        //    // ���ڼ����������������ң����״�������
        //    PlayerController = FindObjectOfType<PlayerController>();
        //    if (PlayerController == null && playerPrefab != null)
        //    {
        //        GameObject player = Instantiate(playerPrefab, _spawnPosition, Quaternion.identity);
        //        PlayerController = player.GetComponent<PlayerController>();
        //    }
        //}
        // �������λ��
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
        if (PlayerController == null) return; // ���������
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
