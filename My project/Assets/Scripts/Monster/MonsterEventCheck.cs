using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEventCheck : MonoBehaviour
{
    public bool anim_play;
    private void AnimationFinish()
    {
        anim_play = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim_play = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
