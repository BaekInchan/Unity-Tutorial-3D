using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolQueue : MonoBehaviour
{
    public Queue<GameObject> objQueue = new Queue<GameObject>();

    public GameObject objPrefab;
    public Transform parent;

    private void Start()
    {
        CreateObj();
    }

    private void CreateObj()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject obj = Instantiate(objPrefab, parent);

            EnqueueObject(obj);
            
        }
    }

    public void EnqueueObject(GameObject newObj )
    {
        newObj.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        newObj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        objQueue.Enqueue(newObj);
        newObj.SetActive(false);
    }

    public GameObject DequeueObject()
    {
        if (objQueue.Count < 10) 
        {
            CreateObj();
        }

        GameObject obj = objQueue.Dequeue();
        obj.SetActive(true);
        
        return obj;
    }
}
