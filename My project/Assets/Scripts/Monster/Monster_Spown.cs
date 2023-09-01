using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���⼱ ���͵��� �޾Ƴ��� ���� ���� ��Ҹ� �޾� �ΰ� �� ��ҿ��� ��ȯ�Ǵ� ������� �ϵ��� ����.
public class Monster_Spown : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monster_list;

    public GameObject[] enemy_summoned_list;

    [SerializeField]
    private Transform[] spown_list;

    [HideInInspector]
    public int spawn_num = 0;

    private GameObject[] player_list;

    public float regen_time = 5.0f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        if(player_list == null)
        {
            player_list = GameObject.FindGameObjectsWithTag("Player");
        }


        for(int i = 0;i<spawn_num;i++)
        {
            enemy_summoned_list[i] = Instantiate(monster_list[Random.Range(0, monster_list.Length)], spown_list[Random.Range(0, spown_list.Length)].position, Quaternion.identity);
        }


        StartCoroutine(Monster_Regen_Check());
    }

    

    // Update is called once per frame
    void Update()
    {
        


    }


    IEnumerator Monster_Regen_Check()
    {
        while(true)
        {
            for(int i = 0;i<spawn_num;i++)
            {
                if (enemy_summoned_list[i] == null)
                {
                    enemy_summoned_list[i] = Instantiate(monster_list[Random.Range(0, monster_list.Length)], spown_list[Random.Range(0, spown_list.Length)].position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(regen_time);
        }
    }
}
