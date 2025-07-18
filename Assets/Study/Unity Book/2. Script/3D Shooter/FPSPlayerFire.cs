using UnityEngine;

public class FPSPlayerFire : MonoBehaviour
{
    public GameObject firePosition;

    public GameObject bombFactory;

    public float throwPower = 15f;

    public GameObject bulletEffect;
    private ParticleSystem ps;

    private void Start()
    {
        ps = bulletEffect.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitInfo)) 
            {
                bulletEffect.transform.position = hitInfo.point;

                ps.Play();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }
    }
}
