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
    public void HpChange(float get_hp)
    {
        hp += get_hp;
    }

    public float SetHp()
    {
        return hp;
    }

    private void Dead()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        if(hp < 0)
        {

        }
    }
}
