using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Default_Removed_item, IShooting
{
    public static int bulletCount = 0;
    private float Life_time;
    private float speed = 30.0f;

    public Vector3 forward_vector;

    public void Awake()
    {
        Life_time = 3.0f;
        bulletCount++;
        //Debug.Log("bulletCount : " + bulletCount);
    }



    public void GetVector(Vector3 forward)
    {
        forward_vector = forward;
    }

    public void Shooting()
    {
        StartCoroutine(BulletLifeCycle());
    }

    private void OnDestroy()
    {
        bulletCount--;
       // Debug.Log("bulletCount : " + bulletCount);
    }

    IEnumerator BulletLifeCycle()
    {
        while(Life_time > 0.0f)
        {
            transform.Translate(forward_vector * speed * Time.deltaTime);
            Life_time -= Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
        
    }
}
