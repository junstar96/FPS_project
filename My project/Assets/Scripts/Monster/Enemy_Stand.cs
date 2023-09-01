using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//스탠드 형은 이동은 추가하지 말고 대신 회전 기능과 공격 기능을 최대한 중점적으로 잡아야 한다.
//스탠드형도 바닥 설치와 천장 설치 중 어느 쪽을 골라야 하는지를 고민해야 한다. 
public class Enemy_Stand : Default_Enemy, IEnemy
{
    EnemyState e_state;

    public float findDistance = 100f;
    public float attackDistance = 2f;

    public GameObject model;
    public Animator animator;

    public MonsterEventCheck monsterEventCheck;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.name == "Golem")
        {
            attackDistance = 20.0f;
        }

        e_state = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        animator = model.GetComponent<Animator>();
        monsterEventCheck = model.GetComponent<MonsterEventCheck>();
    }

    public void StateIdle()
    {
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            e_state = EnemyState.Move;
            Debug.Log("Player Found");
        }
    }
    public void StateRun()
    {
        StateMove();
    }
    public void StateMove()
    {
       
        if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            transform.LookAt(player.position);
        }
        else
        {
            Debug.Log("Attack");
            e_state = EnemyState.Attack;
        }
        //타겟을 찾는 함수
    }
    public void StateAttack()
    {
        animator.SetTrigger("Attack");
        animator.SetInteger("AttackNum", Random.Range(1, 4));
        monsterEventCheck.anim_play = true;
        StartCoroutine(DelayAnim());
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
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
      

        switch(e_state)
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
            case EnemyState.Return:
                StateReturn();
                break;
            case EnemyState.Damaged:
                StateDamaged();
                break;
            case EnemyState.Die:
                StateDie();
                break;
            case EnemyState.Attack:
                StateAttack();
                break;
        }
    }

    IEnumerator DelayAnim()
    {
        yield return new WaitUntil(() => monsterEventCheck.anim_play == false);
        Debug.Log("Finish_check");
        e_state = EnemyState.Idle;
    }
}
