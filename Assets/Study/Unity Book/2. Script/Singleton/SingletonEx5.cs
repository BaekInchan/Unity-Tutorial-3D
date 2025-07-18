using UnityEngine;

public class SingletonEx5 : MonoBehaviour
{
    private static SingletonEx5 instance;
    public static SingletonEx5 Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindFirstObjectByType<SingletonEx5>();

                if(obj != null) // 찾은 경우
                {
                    instance = obj;
                }
                else // 못찾은 경우
                {
                    var newObj = new GameObject("Singleton");
                    newObj.AddComponent<SingletonEx5>();

                    instance = newObj.GetComponent<SingletonEx5>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
