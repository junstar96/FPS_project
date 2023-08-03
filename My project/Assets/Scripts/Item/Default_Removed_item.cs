using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default_Removed_item : MonoBehaviour
{
    public static int count = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        ++count;
        Debug.Log("count : " + count);
    }
    void Start()
    {
        
    }

    public void CountDestroy()
    {
        StartCoroutine(DelayDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        --count;
        Debug.Log("count : " + count);
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
