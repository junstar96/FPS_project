using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
