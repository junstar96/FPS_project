using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public float hp;
    public float maxHp;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
        maxHp = 100;
    }

    //체력이 감소되는 거라면 -값 붙이고 증가하는 것이 디폴트
    public void GetHp(float get_hp)
    {
        hp += get_hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
