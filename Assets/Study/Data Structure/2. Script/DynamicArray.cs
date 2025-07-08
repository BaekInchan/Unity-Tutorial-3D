using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class DynamicArray : MonoBehaviour
{
    public List<int> list1 = new List<int>();

    private void Start()
    {
        

        for (int i = 1; i <= 10; i++)
        {
            list1.Add(i);
        }
        if (list1.Contains(10))
        {
            Debug.Log("값10이 존재");
        }
        else
        {
            Debug.Log("값 10이 존재 X");
        }

    }

}
