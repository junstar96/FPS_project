using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.Play(0);
        Debug.Log("soundCheck");
    }

    private void OnGUI()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
