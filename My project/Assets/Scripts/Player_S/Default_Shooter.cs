using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default_Shooter : MonoBehaviour, IShooting
{
    public Vector3 forward_vector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetVector(Vector3 forward)
    {
        forward_vector = forward;
    }

    public void Shooting()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
