using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : Default_Movement
{
    public Player_Script pInstance;

    public int hp;


    public GameObject bulletFactory;
    public GameObject firePosition;

    public GameObject player_camera;

    public float sensitive_rotate =  30f;

    public float speed = 100;
    // Start is called before the first frame update

    private void Awake()
    {
        hp = 100;
        pInstance = this;
    }
    void Start()
    {
        
    }

    public void Shooting()
    {
        GameObject bullet = Instantiate(bulletFactory);
        bullet.transform.position = transform.position;
        bullet.GetComponent<IShooting>().GetVector(transform.forward);
        bullet.GetComponent<Bullet>().GetUseableType("Player");
        bullet.GetComponent<IShooting>().Shooting();
        
       

        bullet.GetComponent<Bullet>().useabletype = Default_Removed_item.UseableType.Player;
    }

    public void Damaged(int decreaseHp)
    {
        hp -= decreaseHp;
        Debug.Log("HP : " + hp);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shooting();


            //Ray ray = new Ray(transform.position, transform.forward);
            //RaycastHit hitinfo = new RaycastHit();
            //if(Physics.Raycast(ray, out hitinfo))
            //{
            //    Debug.Log("Collision object : " + hitinfo.collider.name);
            //    Debug.Log("Collision Pos : " + hitinfo.point);
                
            //}
        }

        //이동
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

 

        Vector3 movement = Vector3.right * horizontal + Vector3.forward * vertical;

        transform.Translate(movement* speed * Time.deltaTime);

        //회전
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = 0;//Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(-mouseY, mouseX, 0);

        transform.eulerAngles += dir * 90.0f * Time.deltaTime;
    }
}
