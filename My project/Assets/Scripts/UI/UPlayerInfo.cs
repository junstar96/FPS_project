using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UPlayerInfo : MonoBehaviour
{
    public GameObject player; 
    public Player_Script player_script;
    public PlayerHp playerHp;

    //player slider and gradiant
    public Slider hp_slider;
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player != null)
        {
            player_script = player.GetComponent<Player_Script>();
            playerHp = player.GetComponent<PlayerHp>();
        }

        hp_slider = GetComponent<Slider>();

        hp_slider.minValue = 0;
        hp_slider.maxValue = playerHp.maxHp;

        hp_slider.value = playerHp.hp;

        fill.color = gradient.Evaluate(1f);
    }

    void SetPlayerMaxHp(float health)
    {
        hp_slider.maxValue = health;
        hp_slider.value = health;
        fill.color =  gradient.Evaluate(hp_slider.normalizedValue);
    }

    void SetPlayerHp(float health)
    {
        hp_slider.value = health;
        
        fill.color = gradient.Evaluate(hp_slider.normalizedValue);
    }

   
}
