using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float moveSpeed = 100f;
    public bool isMove = true;

    private void Update()
    {
        if (isMove)
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            var closetPos = other.ClosestPoint(transform.position);

            transform.position = closetPos;
            transform.SetParent(other.transform);
            isMove = false;
        }
    }
}
