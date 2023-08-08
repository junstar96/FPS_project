using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

//스탠드 형은 이동은 추가하지 말고 대신 회전 기능과 공격 기능을 최대한 중점적으로 잡아야 한다.
//스탠드형도 바닥 설치와 천장 설치 중 어느 쪽을 골라야 하는지를 고민해야 한다. 
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
        //타겟을 찾는 함수
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
        //공격하는 함수
    }

    public void StateReturn()
    {

    }
    public void StateDamaged()
    {

        //데미지를 받았을 때 어떻게 할 것인지 함수
    }
    public void StateDie()
    {
        //죽을 때 함수
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
