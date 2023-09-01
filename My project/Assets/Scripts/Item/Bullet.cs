using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Bullet : Default_Removed_item, IShooting
{
    public static int bulletCount = 0;
    public float Life_time;
    private float speed = 10.0f;
    public GameObject target; 

    private Rigidbody rb;

    public UseableType useabletype;

    public GameObject particleObject;
    private bool isExplode;
    

    public Vector3 forward_vector;

    public void Awake()
    {
        Life_time = 3.0f;
        bulletCount++;
        isExplode = false;
        useabletype = UseableType.Nothing;

        rb = GetComponent<Rigidbody>();
        //Debug.Log("bulletCount : " + bulletCount);
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bullet collisioncheck : " + collision.gameObject.name);
        if (collision != null)
        {
            switch (useabletype)
            {
                case UseableType.Nothing:
                    //여기는 나중에 처리할 용도
                    break;
                case UseableType.Player:

                    if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        isExplode = true;
                        GameObject pObject = Instantiate(particleObject, transform.parent);
                        pObject.transform.position = transform.position;
                        GetComponent<MeshRenderer>().enabled = false;


                        pObject.GetComponent<ParticleSystem>().Play();
                        Destroy(pObject, 2.0f);
                        bulletCount--;
                        Debug.Log("Enemy_Collision");
                    }
                    else if (collision.gameObject.layer == LayerMask.NameToLayer("Building"))
                    {
                        isExplode = true;
                        GameObject pObject = Instantiate(particleObject, transform.parent);
                        pObject.transform.position = transform.position;
                        GetComponent<MeshRenderer>().enabled = false;


                        pObject.GetComponent<ParticleSystem>().Play();
                        Destroy(pObject, 2.0f);
                        bulletCount--;
                        Debug.Log("Enemy_Collision");
                    }

                    break;
                case UseableType.Enemy:
                    if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
                    {
                        isExplode = true;
                    }
                    break;
            }

        }
    }

    

    public void GetUseableType(string type)
    {
        switch(type)
        {
            case "Player":
                useabletype = UseableType.Player;
                break;
            case "Enemy":
                useabletype = UseableType.Enemy;
                break;
            case "Nothing":
                useabletype = UseableType.Nothing;
                break;
        }
    }

    

    public void GetVector(Vector3 forward)
    {
        forward_vector = forward;
    }

    public void Shooting()
    {
        //Debug.Log("useabletype : " + useabletype);
       switch(useabletype)
        {
            case UseableType.Nothing:
                Life_time = 0f;
                break;
            case UseableType.Player:
                Life_time = 3.0f;
                rb.useGravity = false;
               
                break;
            case UseableType.Enemy:
                Life_time = 2.0f;
                break;
        }
        Destroy(gameObject, Life_time);
        StartCoroutine(BulletLifeCycle());
    }

    private void OnDestroy()
    {
        switch(useabletype)
        {
            case UseableType.Nothing:
                break;
            case UseableType.Player:
                
                //Debug.Log("Destroy Bullet");
                break;
            case UseableType.Enemy:
                break;
        }
        
        
       // Debug.Log("bulletCount : " + bulletCount);
    }

    IEnumerator BulletLifeCycle()
    {
        while (Life_time > 0.0f && !isExplode)
        {
            rb.AddForce(forward_vector * speed, ForceMode.Impulse);
            //transform.Translate(forward_vector * speed * Time.deltaTime);
            Life_time -= Time.deltaTime;

            Ray ray = new Ray(transform.position, forward_vector);

            RaycastHit hitinfo = new RaycastHit();

            if (Physics.Raycast(ray, out hitinfo))
            {
                
                if (hitinfo.distance > 1.0f)
                {
                    Debug.Log("hitinfo_distance" + hitinfo.distance);
                    Debug.Log(hitinfo.collider.tag);
                }
                else
                {
                    if(hitinfo.collider.tag == "Building" || hitinfo.collider.tag == "Enemy" || hitinfo.collider.tag == "Untagged")
                    {
                        isExplode = true;
                        GameObject pObject = Instantiate(particleObject, transform.parent);
                        pObject.transform.position = transform.position;
                        GetComponent<MeshRenderer>().enabled = false;


                        pObject.GetComponent<ParticleSystem>().Play();
                        Destroy(pObject, 2.0f);
                        bulletCount--;
                        Debug.Log("Enemy_Collision_Ray");
                    }
                }
            }

            yield return null;
        }
        Destroy(this.gameObject);

    }
}
