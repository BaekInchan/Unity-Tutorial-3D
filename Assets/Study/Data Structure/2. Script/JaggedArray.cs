using NUnit.Framework;
using UnityEngine;

public class JaggedArray : MonoBehaviour
{
    public int[] array1 = new int[3];
    public int[][] jaggedArray1 = new int[3][];


    private void Start()
    {
        array1[0] = 1;
        array1[1] = 2;
        array1[2] = 3;

        jaggedArray1[0] = new int[3] { 2147483647, 2, 3 };
        jaggedArray1[1] = new int[2] { 1, 2 };
        jaggedArray1[2] = new int[5] { 6, 7, 8, 9, 10 };
    }
}
