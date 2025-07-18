using UnityEngine;

public class PoolItem : MonoBehaviour
{
    private PoolManager poolManager;

    private void Awake()
    {
        poolManager = GameObject.FindFirstObjectByType<PoolManager>();

    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        Invoke("ReturnObject", 3f);
    }

    private void ReturnObject()
    {
        poolManager.pool.Release(gameObject);
    }
}
