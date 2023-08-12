using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    private float shaketime;
    private bool isCoroutine;
    private Vector3 origin_pos;

    // Start is called before the first frame update
    void Start()
    {
        shaketime = 0f;
        origin_pos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShakeCoroutine(float time)
    {
        if (!isCoroutine)
        {
            
            StartCoroutine(Camera_Shake_Time(time));
            isCoroutine = true;
        }
       
    }

    IEnumerator Camera_Shake_Time(float time)
    {
        while(shaketime <= time)
        {
            shaketime += Time.deltaTime;
            transform.localPosition = Random.insideUnitSphere * 0.2f + origin_pos;
            yield return null;
        }
        isCoroutine = false;
        shaketime = 0f;
        transform.localPosition = origin_pos;
    }
}
