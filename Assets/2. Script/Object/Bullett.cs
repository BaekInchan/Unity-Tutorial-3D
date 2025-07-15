using UnityEngine;

public class Bullett : MonoBehaviour
{
    public GameObject bulletFactory;
    public GameObject firePosition;

    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.up;
        transform.position += dir * speed * Time.deltaTime;
    }
}
