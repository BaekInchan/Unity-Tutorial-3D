using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFSM : MonoBehaviour
{
    private enum EnemyState { Idle, Move, Attack, Return, Damaged, Die}
    private EnemyState m_State;

    private Transform player; // Ÿ��
    private CharacterController cc;
    public float findDistance = 8f; // Ž�� �Ÿ�
    public float attackDistance = 3f;
    public float moveSpeed = 5f; // �̵� �ӵ�

    private Animator anim;

    private float currentTime = 0f;
    private float attackDelay = 2f;

    public int attackPower = 3;
    public int hp = 15;

    private Vector3 originPos;
    public float moveDistance = 20f;
    private int maxHp = 15;
    public Slider hpSlider;

    private Quaternion originRot;

    private void Start()
    {
        m_State = EnemyState.Idle;

        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        originPos = transform.position;
        originRot = transform.rotation;
        anim = transform.GetComponentInChildren<Animator>();

        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;

        }
        hpSlider.value = (float)hp / (float)maxHp;
    }
    
    private void Idle()
    {
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            anim.SetTrigger("IdleToMove");
            m_State = EnemyState.Move;
            Debug.Log("���� ��ȯ : Idle -> Move");
        }
    }

    private void Move() 
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            Debug.Log("���� ��ȯ Move -> Return");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance) // Ÿ���� ���� �Ÿ����� �� ��� -> �̵� ����
        {
            Vector3 dir = (player.position - transform.position).normalized;

            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
        }
        else // Ÿ���� ���� �Ÿ� ���� �ִ� ��� ->
        {
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");
            m_State = EnemyState.Attack;
            Debug.Log("���� ��ȯ : Move -> Attack");
        }
    }

    private void Attack() 
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance) // ���� ���� ���� �ִ� ��� ���� ����
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                currentTime = 0f;
                //player.GetComponent<MoveFPSPlayer>().DamageAction(attackPower);
                anim.SetTrigger("StartAttack");
                Debug.Log("����");
            }
        }
        else // ���� ���� �ۿ� ���� ��� -> Move ��ȯ 
        {
            currentTime = 0f;
            anim.SetTrigger("AttackToMove");
            m_State = EnemyState.Move;
            
            Debug.Log("���� ��ȯ : Attack -> Move");
        }
    }
    
    public void AttackAction()
    {
        player.GetComponent<MoveFPSPlayer>().DamageAction(attackPower);

    }

    private void Return() 
    {
        if (Vector3.Distance(transform.position, originPos) > 0.1f) // ���� ��ġ�� ������
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
            transform.forward = dir;
        }
        else
        {
            transform.position = originPos;
            transform.rotation = originRot;
            hp = 15;
            anim.SetTrigger("MoveToIdle");
            m_State = EnemyState.Idle;
            Debug.Log("���� ��ȯ : Return -> Idle");
        }
    }

    public void HitEnemy(int hitPower)
    {
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
            return;
        
        hp -= hitPower;

        if ( hp < 0)
        {
            m_State = EnemyState.Damaged;
            Debug.Log("���� ��ȯ : Any State -> Damaged");
            Damaged();
        }
        else
        {
            m_State = EnemyState.Die;
            Debug.Log("���� ��ȯ : Any State -> Die");
            Die();
        }
    }   
    
    private void Damaged() 
    {
        StartCoroutine(DamageProcess());
    }
    
    IEnumerator DamageProcess()
    {
        
        yield return new WaitForSeconds(0.5f); // �ǰ� �ִϸ��̼� �ð���ŭ ���

        m_State = EnemyState.Move;
        Debug.Log("���� ��ȯ : Damage -> Move");
    }

    private void Die() 
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;
        yield return new WaitForSeconds(2f);
        Debug.Log("�Ҹ�");
        Destroy(gameObject);
    }

}
