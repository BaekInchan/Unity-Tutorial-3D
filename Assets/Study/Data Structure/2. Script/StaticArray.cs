using UnityEngine;

public class StaticArray : MonoBehaviour
{
    public int[] array1; // �迭 ����
    public int[] array2 = { 10, 20, 30, 40, 50 }; // ����� �ʱ�ȭ
    public int[] array3 = new int[5]; // ���� �� ���� �Ҵ�
    public int[] array4 = new int[5] { 10, 20, 30, 40, 50 }; // ����, ���� �Ҵ� �� �ʱ�ȭ

    NewData[] data = new NewData[5];

    void Start()
    {
        int number = array2[3]; // new�� ������ indexer ( ������ȣ )
    }
}


public class NewData
{

}

