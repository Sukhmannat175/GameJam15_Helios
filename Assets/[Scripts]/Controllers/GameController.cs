using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject lantern;
    public static GameController Instance { get; private set; }

    public ShadowMeter shadowMeter;
    public List<ItemSO> tier5;
    public bool isPaused = false;
    public bool shine = true;

    private float rate;
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

    public void Pause(bool pause)
    {
        if (pause)
        {
            isPaused = true;
            rate = shadowMeter.rate;
            Time.timeScale = 0;
            shadowMeter.rate = 0; 
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            shadowMeter.rate = rate;
        }
    }
}
