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

    //ü���� ���ҵǴ� �Ŷ�� -�� ���̰� �����ϴ� ���� ����Ʈ
    public void GetHp(float get_hp)
    {
        hp += get_hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
