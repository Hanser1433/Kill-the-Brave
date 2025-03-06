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
        // ����Ѵ������ʵ���Ҳ������������ٵ�ǰ����
        if (GameController.Instance.PlayerController != null && GameController.Instance.PlayerController != this)
        {
            Destroy(gameObject);
            return; // ȷ���������벻ִ��
        }
        // ����Ϊ�糡����������������
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
            if (animator == null) return; // ���ױ��������
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
        //�������
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interact();
        }
    }
    //��npc�Ľ���
    void Interact()
    {
        Debug.Log("Interact function triggered");  // ���������ȷ�Ϻ���������
        var facingDir = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));//������animator�Ǳ������ֹͣ�ƶ�ʱ������
        var interactPos = (Vector2)transform.position + facingDir; //��������
        Debug.DrawLine(transform.position, interactPos, Color.red, 1f); //����
        var collider = Physics2D.OverlapCircle(interactPos, 0.5f, npcChat);//OverlapCircle���ڴ���һ��Բ�μ������interactPosΪԲ�ģ�npcΪ���Ŀ��
        if (collider != null)
        {
            Debug.Log("there is a npc!");
            collider.GetComponent<Interactable>()?.Interact();//��.Ϊ�ռ����ţ�ȷ��collider��Ϊ�ղŵ���Interact
        }

    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * Time.deltaTime);
    }
}
