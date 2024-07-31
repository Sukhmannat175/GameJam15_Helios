using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string clipName;

    void Start()
    {
        AudioController.Instance.Play(clipName);
    }
}
