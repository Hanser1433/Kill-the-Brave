using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;
    public LayerMask npcChat;
    private void Awake()
    {
        // 如果已存在玩家实例且不是自身，则销毁当前对象
        if (GameController.Instance.PlayerController != null && GameController.Instance.PlayerController != this)
        {
            Destroy(gameObject);
            return; // 确保后续代码不执行
        }
        // 设置为跨场景保留并更新引用
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
        GameController.Instance.PlayerController = this;
    }
    void Start()
    {
        transform.position = GameController.Instance.GetSpawnPoint();
        rb = GetComponent<Rigidbody2D>();
    }
    public void HandleUpdate()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null) return; // 彻底避免空引用
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        bool isWalking = false;
        if (moveX != 0)
            moveY = 0;
        //Debug.Log("X" + moveX);
        //Debug.Log("Y" + moveY);
        if (moveX != 0 || moveY != 0)
        {
            isWalking = true;
            animator.SetFloat("moveX", moveX);
            animator.SetFloat("moveY", moveY);
            animator.SetBool("isWalking", isWalking);
        }
        animator.SetBool("isWalking", isWalking);
        moveDirection = new Vector2(moveX, moveY).normalized * moveSpeed;
        //交互检查
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }
    //与npc的交互
    void Interact()
    {
        Debug.Log("Interact function triggered");  // 调试输出，确认函数被触发
        var facingDir = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));//这里用animator是避免玩家停止移动时不交互
        var interactPos = (Vector2)transform.position + facingDir; //交互区域
        Debug.DrawLine(transform.position, interactPos, Color.red, 1f); //测试
        var collider = Physics2D.OverlapCircle(interactPos, 0.5f, npcChat);//OverlapCircle用于创建一个圆形检测区，interactPos为圆心，npc为检测目标
        if (collider != null)
        {
            Debug.Log("there is a npc!");
            collider.GetComponent<Interactable>()?.Interact();//？.为空检测符号，确保collider不为空才调用Interact
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
    }
}
