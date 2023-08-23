using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class Player_Script : Default_Movement
{
    private Player_Script pInstance;
    private Rigidbody rb;

    //애니메이션 및 총 관련 스크립트
    public Animator animator;
    public Transform hand;
    public GameObject gunObject;
    public GameObject handgun;


    //hp관련 값들
    public int hp;
    public int maxHp;
    public Slider hpSlider;

    

    public GameObject bulletFactory;

    public GameObject player_camera;

    public float sensitive_rotate =  30f;

    public float speed_x = 0.0f;
    public float speed_y = 0.0f;
    public float jumpForce = 10f;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hp = 100;
        maxHp = 100;
        pInstance = this;
    }

    public Player_Script PInstance
    {
        get
        {
            return pInstance;
        }
    }


    void Start()
    {
        
    }

    public void Idle()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.layer == LayerMask.NameToLayer("SolidObject"))
        {
           
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            transform.up = collision.gameObject.transform.up;
        }
    }

    public void Shooting()
    {
        Ray ray = new Ray(player_camera.transform.position, player_camera.transform.forward);

        RaycastHit hitinfo = new RaycastHit();

        if(Physics.Raycast(ray, out hitinfo))
        {
            if(hitinfo.collider.gameObject.layer == LayerMask.NameToLayer("Building"))
            {
                Debug.Log("Collision_Building");
            }
        }


        animator.SetTrigger("Shooting");

        GameObject bullet = Instantiate(bulletFactory, handgun.transform.Find("GunFirePos").position, transform.rotation);
        
        bullet.GetComponent<IShooting>().GetVector(transform.forward);
        bullet.GetComponent<Bullet>().GetUseableType("Player");
        bullet.GetComponent<IShooting>().Shooting();
        handgun.GetComponent<IShooting>().Shooting();
  
        player_camera.GetComponent<Camera_Shake>().ShakeCoroutine(0.1f);
       

        bullet.GetComponent<Bullet>().useabletype = Default_Removed_item.UseableType.Player;
    }

    public void Damaged(int decreaseHp)
    {
        hp -= decreaseHp;
        Debug.Log("HP : " + hp);
    }

    public void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void DrawGun()
    {
        handgun =  Instantiate(gunObject, hand);
        handgun.transform.localPosition = new Vector3(-0.0165999997f, 0.209299996f, 0.0744000003f);
        handgun.transform.localRotation = new Quaternion(-0.409801573f, 0.391794413f, 0.532781601f, 0.628254354f);
        handgun.transform.localScale = new Vector3(0.140660003f, 0.140660003f, 0.140660003f);
        

    }


    private void FixedUpdate()
    {
        //hp체크용
        //hpSlider.value = (float)hp / (float)maxHp;

        

        

    }

    // Update is called once per frame
    void Update()
    {
        
       

        if(Input.GetMouseButtonDown(1))
        {
            DrawGun();
        }

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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
            animator.SetTrigger("Jump");
            animator.SetBool("Grounded", false);
        }
        

        //이동
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Debug.Log("horizontal" + horizontal);

       
        if(vertical == 0)
        {
            animator.SetBool("Vertical_zero", true);
        }
        else
        {
            animator.SetBool("Vertical_zero", false);
        }
       
        speed_x = Mathf.Clamp(speed_x + vertical, -15f, 15f);
        speed_y = Mathf.Clamp(speed_y + horizontal , -15f, 15f);
        animator.SetFloat("Speed_X", speed_x);


        Vector3 movement = new Vector3(player_camera.transform.forward.x * speed_x, player_camera.transform.forward.y, player_camera.transform.forward.z * speed_y);

        //rb.AddForce(movement * speed);

        rb.MovePosition(transform.position + movement *Time.deltaTime);

        //회전
        float mouseX = Input.GetAxis("Mouse X") * 10f;
        float mouseY = 0;//Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(-mouseY, mouseX, 0);

        //transform.eulerAngles += dir * 90.0f * Time.deltaTime;
        rb.rotation = rb.rotation * Quaternion.Euler(dir);
       
    }
}
