using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private float floatSpeed = 1f;//������ʧ�ٶ�
    [SerializeField] private float lifeTime = 1f;//�������ʱ��

    public void Setup(string text, Color color)
    {
        textMesh.text = text;
        textMesh.color = color;
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
