using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Stage_distance_info : MonoBehaviour
{
    public UPlayerInfo playerinfo;
    private GameObject start_point;
    private GameObject end_point;
    private GameObject player;

    public Gradient gradient;
    public Image fill;

    //슬라이더
    public Slider gameslider;

    // Start is called before the first frame update
    void Start()
    {
        playerinfo = GetComponent<UPlayerInfo>();


        start_point = GameObject.FindGameObjectWithTag("Start");
        end_point = GameObject.FindGameObjectWithTag("End");
        player = GameObject.FindGameObjectWithTag("Player");

        //fill.color =  gradient.Evaluate(1f);

        gameslider.minValue = 0;
        gameslider.maxValue = Vector3.Distance(start_point.transform.position, end_point.transform.position);
        gameslider.value = Vector3.Distance(end_point.transform.position, player.transform.position);

        gameslider.SetValueWithoutNotify(12f);

    }

    // Update is called once per frame
    void Update()
    {
        gameslider.value = Vector3.Distance(end_point.transform.position, player.transform.position);
    }
}
