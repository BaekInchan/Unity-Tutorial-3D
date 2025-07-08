using UnityEngine;

public class StudyString : MonoBehaviour
{
    [SerializeField]private string str1 = "Hello* World***";
    [SerializeField]private string[] str2 = new string[3] { "Hello", "Unity", "World" };

    private void Start()
    {

        str1 = str1.Replace("World", "Unity");
        Debug.Log(str1);

        string text = "Apple,Banana,Orange,Melon,Water Melon,Mango,Mandarin";

        string[] fruits = text.Split(',');

        foreach (var fruit in fruits)
            Debug.Log(fruit);
        
    }
}
