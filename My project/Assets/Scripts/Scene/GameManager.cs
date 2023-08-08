using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public bool isGameOver;

    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                return null;
            }
            return instance;
        }

    }

    public void InitGame()
    {

    }

    public  void PauseGame()
    {

    }

    public void ContinueGame()
    {

    }

    public void RestartGame()
    {

    }

    public void StopGame()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
