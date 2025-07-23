using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    private enum EnemyState { Idle, Move, Attack, Return, Damaged, Die}
    private EnemyState m_State;

    private Transform player; // 타겟
    private CharacterController cc;
    public float findDistance = 8f; // 탐지 거리
    public float attackDistance = 3f;
    public float moveSpeed = 5f; // 이동 속도

    private Animator anim;

    private float currentTime = 0f;
    private float attackDelay = 2f;

    public int attackPower = 3;
    public int hp = 15;
    private int maxHp = 15;

    public Slider hpSlider;

    public float moveDistance = 20f;
    private Vector3 originPos;
    private Quaternion originRot;

    NavMeshAgent smith;

    private void Start()
    {
        m_State = EnemyState.Idle;

        player = GameObject.Find("Player").transform;
        cc = GetComponent<CharacterController>();
        originPos = transform.position;
        originRot = transform.rotation;
        anim = transform.GetComponentInChildren<Animator>();

        smith = GetComponent<NavMeshAgent>();
        
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
            Debug.Log("상태 전환 : Idle -> Move");
        }
    }

    private void Move() 
    {
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            m_State = EnemyState.Return;
            Debug.Log("상태 전환 Move -> Return");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance) // 타겟이 공격 거리보다 먼 경우 -> 이동 실행
        {
            //Vector3 dir = (player.position - transform.position).normalized;

            //cc.Move(dir * moveSpeed * Time.deltaTime);
            //transform.forward = dir;

            smith.stoppingDistance = attackDistance;
            smith.SetDestination(player.position);


            
        }
        else // 타겟이 공격 거리 내에 있는 경우 -> 공격 전환
        {
            m_State = EnemyState.Attack;
            Debug.Log("상태 전환 : Move -> Attack");
            currentTime = attackDelay;
            anim.SetTrigger("MoveToAttackDelay");
            smith.isStopped = true;
            smith.ResetPath();
        }
        if (!smith.isOnNavMesh)
        {
            Debug.LogWarning("Agent가 NavMesh 위에 없음!");
        }

    }

    private void Attack() 
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance) // 공격 법위 내에 있는 경우 공격 실행
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                currentTime = 0f;
                //player.GetComponent<MoveFPSPlayer>().DamageAction(attackPower);
                anim.SetTrigger("StartAttack");
                Debug.Log("공격");
            }
        }
        else // 공격 범위 밖에 있을 경우 -> Move 전환 
        {
            currentTime = 0f;
            anim.SetTrigger("AttackToMove");
            m_State = EnemyState.Move;
            
            Debug.Log("상태 전환 : Attack -> Move");
        }
    }
    
    public void AttackAction()
    {
        player.GetComponent<MoveFPSPlayer>().DamageAction(attackPower);

    }

    private void Return() 
    {
        if (Vector3.Distance(transform.position, originPos) > 0.5f) // 원래 위치로 복귀중
        {
            //Vector3 dir = (originPos - transform.position).normalized;
            //cc.Move(dir * moveSpeed * Time.deltaTime);
            //transform.forward = dir;
            smith.isStopped = false;
            smith.SetDestination(originPos);
            smith.stoppingDistance = 0;
        }
        else
        {
            smith.isStopped = true;
            smith.ResetPath();

            transform.position = originPos;
            transform.rotation = originRot;
            
            hp = maxHp;
            m_State = EnemyState.Idle;
            Debug.Log("상태 전환 : Return -> Idle");
            anim.SetTrigger("MoveToIdle");
        }
    }

    public void HitEnemy(int hitPower)
    {
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
            return;
        
        hp -= hitPower;

        smith.isStopped = true;
        smith.ResetPath();

        if ( hp > 0)
        {
            m_State = EnemyState.Damaged;
            Debug.Log("상태 전환 : Any State -> Damaged");
            anim.SetTrigger("Damaged");
            Damaged();
        }
        else
        {
            m_State = EnemyState.Die;
            Debug.Log("상태 전환 : Any State -> Die");
            anim.SetTrigger("Die");
            Die();
        }
    }   
    
    private void Damaged() 
    {
        StartCoroutine(DamageProcess());
    }
    
    IEnumerator DamageProcess()
    {
        
        yield return new WaitForSeconds(1f); // 피격 애니메이션 시간만큼 대기

        m_State = EnemyState.Move;
        Debug.Log("상태 전환 : Damage -> Move");
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
        Debug.Log("소멸");
        Destroy(gameObject);
    }

}
