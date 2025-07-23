using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngineInternal;

public class MoveFPSPlayer : MonoBehaviour
{
    private CharacterController cc;

    public float moveSpeed = 7f;

    private float gravity = -20f;
    private float yVelocity = 0f;

    public float jumpPower = 10f;
    public bool isJumping = false;

    public int hp = 20;

    Animator anim;

    private int maxHp = 20;
    public Slider hpSlider;

    public GameObject hitEffect;

    private void Start()
    {
        cc = GetComponent<CharacterController>();

        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (FPSGameManager.Instance.gState != FPSGameManager.GameState.Run)
            return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        anim.SetFloat("MoveMotion", dir.magnitude);

        dir = Camera.main.transform.TransformDirection(dir);

        transform.position += dir * moveSpeed * Time.deltaTime;

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        if(cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
            }
            yVelocity = 0f;
        }

        if (Input.GetButtonDown("Jump"))
        {

            isJumping = true;
            yVelocity = jumpPower;
        }
    }

    public void DamageAction(int damage)
    {
        hp -= damage;


        hpSlider.value = (float)hp / (float)maxHp;

        if (hp > 0)
        {
            StartCoroutine(PlayHitEffect());
        }
    }

    IEnumerator PlayHitEffect()
    {
        hitEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        hitEffect.SetActive(false);
    }
}
