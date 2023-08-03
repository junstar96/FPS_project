using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

//스탠드 형은 이동은 추가하지 말고 대신 회전 기능과 공격 기능을 최대한 중점적으로 잡아야 한다.
//스탠드형도 바닥 설치와 천장 설치 중 어느 쪽을 골라야 하는지를 고민해야 한다. 
public class Enemy_Movable : Default_Enemy, IEnemy
{
    EnemyState e_state;

    private float currentTime = 0f;

    public float findDistance = 8f;
    public float attackDistance = 2f;

    public float attackDelay = 2f;
    public float attackSpeed = 1f;

    CharacterController cc;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        e_state = EnemyState.Idle;

        player = GameObject.Find("Player").transform;

        cc = GetComponent<CharacterController>();

        attackPower = 3;
    }

    public void StateIdle()
    {
        if (Vector3.Distance(transform.position, player.position) < findDistance)
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
            Vector3 dir = (player.position - transform.position).normalized;
            cc.Move(dir * Time.deltaTime);
            currentTime += Time.deltaTime;
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
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                currentTime = 0f;
                player.GetComponent<Player_Script>().Damaged(attackPower);
            }
        }
        else
        {

            e_state = EnemyState.Move;
            currentTime = 0f;
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
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {



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
    }
}
