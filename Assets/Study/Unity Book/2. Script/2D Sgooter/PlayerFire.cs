#define DEBUG_TEST

using System.Collections.Generic;
using UnityEngine;


public class PlayerFire : Singleton<PlayerFire>
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    public int poolSize = 10;

    // public GameObject[] bulletObjectPool; // 배열
    // public List<GameObject> bulletObjectPool; // 리스트
    public Queue<GameObject> bulletObjectPool; // 큐

    void Start()
    {
        // bulletObjectPool = new GameObject[poolSize]; // 배열
        // bulletObjectPool = new List<GameObject>(); // 리스트
        bulletObjectPool = new Queue<GameObject>(); // 큐

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletFactory);

            // bulletObjectPool[i] = bullet; // 배열
            // bulletObjectPool.Add(bullet); // 리스트
            bulletObjectPool.Enqueue(bullet); // 큐

            bullet.SetActive(false);
        }
    }

    void Update()
    {
#if UNITY_STANDARDALONE || UNITY_EDITOR || DEBUG_TEST
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("마우스 클릭");
            // 큐
            if (bulletObjectPool.Count > 0)
            {
                GameObject bullet = bulletObjectPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = firePosition.transform.position;
            }
        }

#elif UNITY_ANDROID
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("손가락터치");
            if (bulletObjectPool.Count > 0)
            {
                GameObject bullet = bulletObjectPool.Dequeue();
                bullet.SetActive(true);
                bullet.transform.position = firePosition.transform.position;
            }
        }
#else
        Debug.Log("그외 나머지 플랫폼");
#endif

    }
}