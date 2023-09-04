using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//스탠드 형은 이동은 추가하지 말고 대신 회전 기능과 공격 기능을 최대한 중점적으로 잡아야 한다.
//스탠드형도 바닥 설치와 천장 설치 중 어느 쪽을 골라야 하는지를 고민해야 한다. 
public class Enemy_Movable : Default_Enemy, IEnemy
{
    public GameObject EnemyModeling;
    public Animator animator;


    NavMeshAgent navMeshAgent;

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

    //public Slider hpbar;

    public MonsterEventCheck eventcheck;



    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        animator = EnemyModeling.GetComponent<Animator>();
        e_state = EnemyState.Idle;

        navMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player");
       

        attackPower = 3;
    }

    public void StateIdle()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        if (Vector3.Distance(transform.position, player.transform.position) < findDistance)
        {
            animator.SetBool("Found_Player", true);

            Debug.Log("IdleCheck");

            //여긴 나중에 다시 손 보고 지금은 navmesh 체크
           if(e_state == EnemyState.Idle)
            {
                if(eventcheck.anim_play == false)
                {
                    e_state = EnemyState.Move;
                }
                
            }


            //anim_time += Time.deltaTime;


            //Debug.Log("Player Found");


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

    //좀비
    public void StateMove()
    {
        if(this.gameObject.tag == "Zomble")
        {
            transform.LookAt(player.transform);
            navMeshAgent.destination = player.transform.position ;
            

        }


        transform.LookAt(player.transform);

        speed = Mathf.Clamp(speed + Time.deltaTime, 0.0f, 10.0f);
        animator.SetFloat("Speed", speed);

        if (Vector3.Distance(transform.position, player.transform.position) > attackDistance)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < findDistance)
            {
                navMeshAgent.isStopped = false;

                navMeshAgent.stoppingDistance = attackDistance;
                navMeshAgent.speed = speed;
                navMeshAgent.destination = player.transform.position;


                Vector3 dir = (player.transform.position - transform.position).normalized;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
        anim_time += Time.deltaTime;

        currentTime += Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {
            if(currentTime > attackDelay)
            {
                navMeshAgent.isStopped = true;
                navMeshAgent.ResetPath();
                currentTime = 0f;
                player.GetComponent<Player_Script>().Damaged(attackPower);
                animator.SetTrigger("Attack");
                if(!delaycheck)
                {
                    if(gameObject.tag == "Zombie")
                    {
                        
                        StartCoroutine(CheckDelay(5.25f, EnemyState.Move));
                    }
                    
                    delaycheck = true;
                }

            }
        }
        else
        {
            if(Vector3.Distance(transform.position, player.transform.position) > findDistance)
            {
                animator.SetBool("Found_Player", false);
                e_state = EnemyState.Idle;
            }
            else
            {
                if(!delaycheck)
                {
                    if (currentTime > attackDelay)
                    {
                        e_state = EnemyState.Move;
                        Debug.Log("AttackToMove");
                        currentTime = 0f;
                    }
                }
                
               
            }
        }
        //공격하는 함수
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision == null)
        {
            Debug.Log("not collision");
        }
        else
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Application.Quit();
            }
        }
    }



    public void StateReturn()
    {

    }
    public void StateDamaged()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        animator.SetInteger("HitMotionNum", Random.Range(1, 2));
        animator.SetTrigger("HitTrigger");
    

        if (!delaycheck)
        {
            if(gameObject.name == "Enemy_Zombie")
            {
                StartCoroutine(CheckDelay(2f, EnemyState.Idle));
            }
        }
        
        //데미지를 받았을 때 어떻게 할 것인지 함수
    }
    public void StateDie()
    {
        
        //죽을 때 함수
        Destroy(gameObject, 3f);

    }

    private void FixedUpdate()
    {
        //hpbar.value = (float)hp / (float)maxHp;
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



        animator.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
        animator.SetInteger("HP", (int)GetComponent<MonsterHp>().hp);

    }

    IEnumerator CheckDelay(float delay, EnemyState state)
    {
        yield return null;


        yield return new WaitUntil(() => eventcheck.GetAnimationPlayCheck() == false);

        yield return new WaitForSeconds(delay);
        e_state = state;
    }

    IEnumerator PlayDeltaTimer()
    {
        while(anim_time < animator.GetCurrentAnimatorClipInfo(0).Length)
        {
            anim_time += Time.deltaTime;
            yield return null;
        }
        delaycheck = false;
    }
}
