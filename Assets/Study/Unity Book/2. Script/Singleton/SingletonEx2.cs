using UnityEngine;

public class SingletonEx2 : MonoBehaviour
{
    public static SingletonEx2 Instance
    {
        get; // ���� ����
        private set; // ���� �Ұ�
    }

    private void Awake()
    {
        if (Instance == null) // Instace�� ��������� �ڽ��� �Ҵ� 
        {
            Instance = this;
        }
        else // �̹� SingletonEx�� �����ϸ� this ��ũ��Ʈ ����
        {
            Destroy(gameObject);
        }
    }
}
