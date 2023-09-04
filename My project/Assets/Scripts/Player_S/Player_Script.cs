using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UI;

public class Player_Script : Default_Movement
{
    [HideInInspector]
    public static Player_Script pInstance;
    private Rigidbody rb;
    private PlayerHp playerhp;

    //애니메이션 및 총 관련 스크립트
    public Animator animator;

    public Transform hand;

    public GameObject gunObject;

    [HideInInspector]
    public GameObject handgun;


    [HideInInspector]
    public CameraManager cameraManager;

    

    public GameObject bulletFactory;

    public GameObject player_camera;

    public float sensitive_rotate =  30f;

    public float speed_x = 0.0f;
    public float speed_y = 0.0f;
    public float jumpForce = 10f;
    public bool isShooting;
    // Start is called before the first frame update

    private bool moveKeyInput = false;

    private void Awake()
    {
        if(pInstance == null)
        {
            pInstance = this;
        }


        rb = GetComponent<Rigidbody>();
        cameraManager = GetComponent<CameraManager>();
        isShooting = false;
        pInstance = this;
        playerhp = GetComponent<PlayerHp>();
    }

    public static Player_Script PInstance
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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            player_camera = cameraManager.CameraChange();
        }
        


        if (Input.GetMouseButtonDown(0))
        {
            if(handgun == null)
            {
                DrawGun();
            }
            
            Shooting();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if(!isShooting)
        {
            
            //회전
            float mouseX = Input.GetAxis("Mouse X") * 10f;
            float mouseY = 0;

            Vector3 dir = new Vector3(-mouseY, mouseX, 0);

            rb.rotation = rb.rotation * Quaternion.Euler(dir);


            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");


            speed_x = vertical * 10f;
            speed_y = horizontal * 10f;
            animator.SetFloat("Speed_X", speed_x);
            animator.SetFloat("Speed_Y", speed_y);



            //Vector3 movement = player_camera.transform.forward * speed_x + player_camera.transform.right * speed_y;
            Vector3 movement = transform.forward* speed_x + transform.right * speed_y;
            //movement = Quaternion.Euler(dir) * movement;

            rb.velocity = movement;
        }
        //이동



       
       
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

    public void TakeDamage(float damage)
    {
        
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
