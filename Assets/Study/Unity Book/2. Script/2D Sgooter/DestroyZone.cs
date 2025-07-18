using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Bullet"))
        {
            PlayerFire.Instance.bulletObjectPool.Enqueue(other.gameObject);
            other.gameObject.SetActive(false); 
        }
        else
            other.gameObject.SetActive(false);
    }
}
