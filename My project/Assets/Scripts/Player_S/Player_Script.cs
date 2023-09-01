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




    

    public GameObject bulletFactory;

    public GameObject player_camera;

    public float sensitive_rotate =  30f;

    public float speed_x = 0.0f;
    public float speed_y = 0.0f;
    public float jumpForce = 10f;
    // Start is called before the first frame update

    private bool moveKeyInput = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
        Debug.Log("idlecheck");
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

    }

    public void Jump()
    {
        //rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        animator.SetBool("Grounded", false);
        StartCoroutine(JumpCheck());
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
        Debug.Log(speed_x + " check " + speed_y);

        if(!moveKeyInput)
        {
            if (speed_x > 0)
            {
                speed_x -= Time.deltaTime;
            }
            else if (speed_x < 0)
            {
                speed_x += Time.deltaTime;
            }
            else if (speed_x < 0.1f && speed_x > -0.1f)
            {
                speed_x = 0f;
            }

            if (speed_y > 0)
            {
                speed_y -= Time.deltaTime;
            }
            else if (speed_y < 0)
            {
                speed_y += Time.deltaTime;
            }
            else if (speed_y < 0.1f && speed_y > -0.1f)
            {
                speed_y = 0f;
            }
        }



        if (Input.GetMouseButtonDown(0))
        {
            if(handgun == null)
            {
                DrawGun();
            }
            
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
            Jump();
        }
        

        //이동
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");


        //if(Input.GetKey(KeyCode.W))
        //{
        //    moveKeyInput = true;
        //    speed_y = Mathf.Clamp(speed_y - Time.deltaTime, -15f, 15f);
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    moveKeyInput = true;
        //    speed_x = Mathf.Clamp(speed_x - Time.deltaTime, -15f, 15f);
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    moveKeyInput = true;
        //    speed_y = Mathf.Clamp(speed_y + Time.deltaTime, -15f, 15f);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    moveKeyInput = true;
        //    speed_x = Mathf.Clamp(speed_x + Time.deltaTime, -15f, 15f);
        //}



        speed_x = Mathf.Clamp(speed_x + vertical, -5f, 5f);
        speed_y = Mathf.Clamp(speed_y + horizontal, -5f, 5f);
        animator.SetFloat("Speed_X", speed_x);
        animator.SetFloat("Speed_Y", speed_y);

       

        Vector3 movement = new Vector3(player_camera.transform.forward.x * speed_x, player_camera.transform.forward.y, player_camera.transform.forward.z * speed_y);
        ////Vector3 movement = new Vector3(player_camera.transform.forward.x * speed_x, 0, player_camera.transform.forward.z * speed_y).normalized;

        rb.AddForce(movement);

        //transform.Translate(movement * 10);

        //rb.MovePosition(transform.position + movement);

        //회전
        float mouseX = Input.GetAxis("Mouse X") * 10f;
        float mouseY = 0;//Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(-mouseY, mouseX, 0);

        //transform.eulerAngles += dir * 90.0f * Time.deltaTime;
        rb.rotation = rb.rotation * Quaternion.Euler(dir);
       
    }

    private void JumpStay()
    {
        Debug.Log("checkJump");
    }

    IEnumerator JumpCheck()
    {
        yield return new WaitForSeconds(1.0f);
        

        while(true)
        {
            Ray ray = new Ray(transform.position, Vector3.down);

            RaycastHit raycasthit = new RaycastHit();
            if(Physics.Raycast(ray, out raycasthit))
            {
                if(raycasthit.distance < 1.0f)
                {
                    Debug.Log("checkHello");
                    animator.speed = 2f;
                    break;

                }
            }
            yield return null;
        }
    }


    IEnumerator SpeedDown()
    {
        while(true)
        {
            if(moveKeyInput)
            {
                break;
            }
            yield return null;
        }
        
    }
}
