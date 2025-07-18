using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CrossBow : MonoBehaviour
{


    // ȭ���� �߻��ϴ� ���
    // ȭ��
    // �߻��� ��ġ
    // ���ư��� ���

    public GameObject arrowPrefab;
    public Transform shootPos;

    public bool isShoot;
    
    // Arrow ��ũ��Ʈ ����

    private void Update()
    {
        Ray ray = new Ray(shootPos.position, shootPos.forward);
        RaycastHit hit; // ������ ���� ���

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
    // �������� �����ϴ� ���
    // ������(Raycast) -> �������� �� ȭ���� ���� -> ������ ȭ���� ���ư�
    



