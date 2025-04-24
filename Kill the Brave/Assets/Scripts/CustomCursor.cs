using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [Header("�������")]
    public Texture2D cursorTexture;  // ������Ĺ���ز�
    public Vector2 hotSpot = Vector2.zero;  // �������ƫ�ƣ������ͷ���λ�ã�

    void Start()
    {
        // ����ϵͳ���
        Cursor.visible = false;

        // �����Զ�����
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void OnDestroy()
    {
        // �ָ�Ĭ�Ϲ��
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
    }
}