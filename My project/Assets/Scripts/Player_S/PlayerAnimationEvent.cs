using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Player_Script ps;

    

    private void JumpStay()
    {
        ps.animator.speed = 0.001f;
    }

    // Start is called before the first frame update
    void Start()
    {
        ps = transform.parent.GetComponent<Player_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
