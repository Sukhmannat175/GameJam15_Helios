using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string playName;
    public string stopName;

    void Start()
    {
        AudioController.Instance.Play(playName);
        AudioController.Instance.Stop(stopName);
    }
}
