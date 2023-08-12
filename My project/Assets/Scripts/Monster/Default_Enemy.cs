using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Default_Enemy : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int level;
    public int attackPower;
    public enum EnemyState
    {
        Idle,
        Run,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    // Start is called before the first frame update



    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
