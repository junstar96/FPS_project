using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour, IShooting
{
    public Transform FirePos;
    public GameObject particleObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shooting()
    {
        GameObject gunfire = Instantiate(particleObject, FirePos.transform.position, FirePos.transform.rotation);
        gunfire.GetComponent<ParticleSystem>().Play();
        Destroy(gunfire, 0.1f);

       
    }

    public void GetVector(Vector3 pos)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
