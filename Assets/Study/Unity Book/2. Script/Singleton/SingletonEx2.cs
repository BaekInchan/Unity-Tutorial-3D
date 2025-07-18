using UnityEngine;

public class SingletonEx2 : MonoBehaviour
{
    public static SingletonEx2 Instance
    {
        get; // 접근 가능
        private set; // 수정 불가
    }

    private void Awake()
    {
        if (Instance == null) // Instace가 비어있으면 자신을 할당 
        {
            Instance = this;
        }
        else // 이미 SingletonEx가 존재하면 this 스크립트 삭제
        {
            Destroy(gameObject);
        }
    }
}
