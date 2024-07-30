using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    public GameObject lantern;


    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangeWorlds(int worldNum)
    {

    }

    public void Pause(bool pause)
    {
        if (pause)
        {

        }
    }
}
