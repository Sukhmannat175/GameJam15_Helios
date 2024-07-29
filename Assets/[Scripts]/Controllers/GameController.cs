using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameController : MonoBehaviour
{
    public GameObject crafterMenu;
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
    }

    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            crafterMenu.SetActive(!crafterMenu.activeInHierarchy);
        }
    }

    public void ChangeWorlds(int worldNum)
    {

    }
}
