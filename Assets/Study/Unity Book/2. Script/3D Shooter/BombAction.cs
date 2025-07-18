using UnityEngine;

public class BombAction : MonoBehaviour
{
    public GameObject bombEffect;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject eff = Instantiate(bombEffect);
        eff.transform.position = transform.position; // 파티클 위치 초기화

        Destroy(gameObject);
    }
}
