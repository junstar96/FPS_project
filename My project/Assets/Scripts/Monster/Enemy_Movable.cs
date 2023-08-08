using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

//���ĵ� ���� �̵��� �߰����� ���� ��� ȸ�� ��ɰ� ���� ����� �ִ��� ���������� ��ƾ� �Ѵ�.
//���ĵ����� �ٴ� ��ġ�� õ�� ��ġ �� ��� ���� ���� �ϴ����� ����ؾ� �Ѵ�. 
public class Enemy_Movable : Default_Enemy, IEnemy
{
    public GameObject EnemyModeling;
    public Animator animator;


    EnemyState e_state;
    [HideInInspector]
    public float speed = 10.0f;
    private float currentTime = 0f;

    public float findDistance = 80f;
    public float attackDistance = 2f;

    public float attackDelay = 2f;
    public float attackSpeed = 1f;

    public bool delaycheck = false;
    public float anim_time = 0.0f;

    CharacterController cc;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        animator = EnemyModeling.GetComponent<Animator>();
        e_state = EnemyState.Idle;

        player = GameObject.Find("Player").transform;
       

        cc = GetComponent<CharacterController>();

        attackPower = 3;
    }

    public void StateIdle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            animator.SetBool("Found_Player", true);
            anim_time += Time.deltaTime;
            if(anim_time >= animator.GetCurrentAnimatorClipInfo(0).Length)
            {
                delaycheck = !delaycheck;
                e_state = EnemyState.Move;
                anim_time = 0f;
            }


            


            Debug.Log("Player Found");
           
        }
        else
        {
            e_state = EnemyState.Idle;
            animator.SetBool("Found_Player", false);
            Debug.Log("Player Missing");
        }
    }
    public void StateRun()
    {
        StateMove();
    }
    public void StateMove()
    {
        
        transform.LookAt(player);
        speed = Mathf.Clamp(speed + Time.deltaTime, 0.0f, 10.0f);
        animator.SetFloat("Speed", speed);
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
           if(Vector3.Distance(transform.position, player.position) < findDistance)
            {
                Vector3 dir = (player.position - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                currentTime += Time.deltaTime;
                
                e_state = EnemyState.Move;
            }
            else
            {
                animator.SetBool("Found_Player", false);
                e_state = EnemyState.Idle;
            }
        }
        else
        {
            e_state = EnemyState.Attack;
            Debug.Log("Player attack form");
        }
        //Ÿ���� ã�� �Լ�
    }
    public void StateAttack()
    {
        if(currentTime  >= 0.01f)
        {
            anim_time = 0.0f;
        }
        anim_time = Time.deltaTime;


        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;

            
            if(currentTime > attackDelay)
            {
                currentTime = 0f;
                player.GetComponent<Player_Script>().Damaged(attackPower);
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, player.position) > findDistance)
            {
                animator.SetBool("Found_Player", false);
                e_state = EnemyState.Idle;
            }
            else
            {
                if(anim_time > animator.GetCurrentAnimatorClipInfo(0).Length)
                {
                    e_state = EnemyState.Move;
                    currentTime = 0f;
                }
            }
        }
        //�����ϴ� �Լ�
    }

    public void StateReturn()
    {

    }
    public void StateDamaged()
    {

        //�������� �޾��� �� ��� �� ������ �Լ�
    }
    public void StateDie()
    {
        //���� �� �Լ�
        Destroy(gameObject, 3f);

    }

    // Update is called once per frame
    void Update()
    {
       
        Debug.Log("state :" + e_state);
        switch (e_state)
        {
            case EnemyState.Idle:
                StateIdle();
                break;
            case EnemyState.Run:
                StateRun();
                break;
            case EnemyState.Move:
                StateMove();
                break;
            case EnemyState.Attack:
                StateAttack();
                break;
            case EnemyState.Return:
                StateReturn();
                break;
            case EnemyState.Damaged:
                StateDamaged();
                break;
            case EnemyState.Die:
                StateDie();
                break;

        }

        
        

        animator.SetInteger("HP", hp);
       

    }

    IEnumerator CheckDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        delaycheck = true;
    }
}
