using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] camera_list;
    public GameObject main_camera;

    public int camera_num;

    // Start is called beforem the first frame update
    void Start()
    {
        camera_num = 0;
        main_camera = camera_list[camera_num];

        for(int i = 1;i<camera_list.Length;i++)
        {
            camera_list[i].SetActive(false);
        }
    }

    public GameObject CameraChange()
    {
       
        camera_num++;
        camera_num = camera_num % camera_list.Length;

        GameObject temp = camera_list[camera_num];
        main_camera.SetActive(false);
        main_camera = temp;
        main_camera.SetActive(true);
        main_camera = camera_list[camera_num];

        return main_camera;
    }

    // Update is called once per frame
   
}
