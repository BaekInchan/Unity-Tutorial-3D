using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CrossBow : MonoBehaviour
{


    // 화살을 발사하는 기능
    // 화살
    // 발사할 위치
    // 날아가는 기능

    public GameObject arrowPrefab;
    public Transform shootPos;

    public bool isShoot;
    
    // Arrow 스크립트 생성

    private void Update()
    {
        Ray ray = new Ray(shootPos.position, shootPos.forward);
        RaycastHit hit; // 레이저 닿은 대상

        bool isTargeting = Physics.Raycast(ray, out hit);

        Debug.DrawRay(shootPos.position, transform.forward * 100f, Color.green);

        if (isTargeting && !isShoot)
            StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        isShoot = true;

        GameObject arrow = Instantiate(arrowPrefab);
        Quaternion rot = Quaternion.Euler(new Vector3(90, 0, 0));

        arrow.transform.SetPositionAndRotation(shootPos.position, rot);
        yield return new WaitForSeconds(3f);

        isShoot = false;
        //arrow.transform.position = shootPos.position;
        //arrow.transform.rotation = Quaternion.identity;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootPos.position, shootPos.forward * 100f);
    }
}
    // 누군가를 감지하는 기능
    // 직선상(Raycast) -> 감지했을 때 화살을 생성 -> 생성한 화살이 날아감
    



