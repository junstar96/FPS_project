using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

//���ĵ� ���� �̵��� �߰����� ���� ��� ȸ�� ��ɰ� ���� ����� �ִ��� ���������� ��ƾ� �Ѵ�.
//���ĵ����� �ٴ� ��ġ�� õ�� ��ġ �� ��� ���� ���� �ϴ����� ����ؾ� �Ѵ�. 
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
        //Ÿ���� ã�� �Լ�
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
