using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour
{
    public float hp;
    public float maxHp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Demaged(float damage)
    {
        hp -= damage;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
