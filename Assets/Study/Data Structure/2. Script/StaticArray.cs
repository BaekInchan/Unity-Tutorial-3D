using UnityEngine;

public class StaticArray : MonoBehaviour
{
    public int[] array1; // 배열 선언
    public int[] array2 = { 10, 20, 30, 40, 50 }; // 선언과 초기화
    public int[] array3 = new int[5]; // 선언 및 공간 할당
    public int[] array4 = new int[5] { 10, 20, 30, 40, 50 }; // 선언, 공간 할당 및 초기화

    NewData[] data = new NewData[5];

    void Start()
    {
        int number = array2[3]; // new가 없으면 indexer ( 순서번호 )
    }
}


public class NewData
{

}

