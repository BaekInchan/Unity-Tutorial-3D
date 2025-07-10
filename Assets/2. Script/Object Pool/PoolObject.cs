using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private ObjectPoolQueue pool;

    public float bulletSpeed = 100f;

    void Awake()
    {
        pool = FindFirstObjectByType<ObjectPoolQueue>();
    }

    private void OnEnable()
    {
        Invoke("ReturnPool", 3f);
    }

    private void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * bulletSpeed;
    }

    private void ReturnPool()
    {
        pool.EnqueueObject(gameObject);
    }

}
