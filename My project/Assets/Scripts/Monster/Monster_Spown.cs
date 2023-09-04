using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//여기선 몬스터들을 받아놓고 생산 라인 장소를 받아 두고 그 장소에서 소환되는 방식으로 하도록 하자.
public class Monster_Spown : MonoBehaviour
{
    [SerializeField]
    private GameObject WaveZombie;

    public GameObject[] enemy_summoned_list;

    [SerializeField]
    private Transform spawnPoint;

    [HideInInspector]
    public int spawn_num = 0;

    private GameObject player;

    public float regen_time = 5.0f;

    public Transform[] summon_point;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        spawn_num = 50;


        for(int i = 0;i<spawn_num;i++)
        {
            enemy_summoned_list[i] =  Instantiate(WaveZombie, spawnPoint.position, Quaternion.identity);
            enemy_summoned_list[i].GetComponent<Enemy_Movable>().findDistance = 1000.0f;
        }


        //StartCoroutine(Monster_Regen_Check());
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
                    //enemy_summoned_list[i] = Instantiate(monster_list[Random.Range(0, monster_list.Length)], spown_list[Random.Range(0, spown_list.Length)].position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(regen_time);
        }
    }
}
