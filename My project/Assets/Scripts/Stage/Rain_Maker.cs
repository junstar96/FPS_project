using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rain_Maker : MonoBehaviour
{
    public GameObject rain;
    public GameObject[] rainlist = new GameObject[100];
    public float make_time;
    // Start is called before the first frame update
    void Start()
    {
        make_time = 3.0f;
        StartCoroutine(MakeAndDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MakeAndDestroy()
    {
        while(true)
        {
            int make_num = Random.Range(1, 100);
            for (int i = 0; i < make_num; i++)
            {
                GameObject drop_rain = Instantiate(rain);
                float drop_x = Random.Range(-1000.0f, 1000.0f);
                float drop_z = Random.Range(-1000.0f, 1000.0f);

                drop_rain.transform.position = new Vector3(drop_x, transform.position.y, drop_z);

                drop_rain.AddComponent<Rigidbody>();

                if(drop_rain.GetComponent<Default_Removed_item>() != null)
                {
                    //Debug.Log("destroy_count");
                    drop_rain.GetComponent<Default_Removed_item>().CountDestroy();
                }
            }

            yield return new WaitForSeconds(make_time);
            make_time = Random.Range(2.0f, 6.0f);
        }
        

    }
}
