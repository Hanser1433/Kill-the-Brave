using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    [Header("光标设置")]
    public Texture2D cursorTexture;  // 拖入你的光标素材
    public Vector2 hotSpot = Vector2.zero;  // 点击热区偏移（例如箭头尖的位置）

    void Start()
    {
        // 隐藏系统光标
        Cursor.visible = false;

        // 设置自定义光标
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    void OnDestroy()
    {
        // 恢复默认光标
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
    }
}