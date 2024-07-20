using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSO item;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        name = item.name;
        sr.sprite = item.icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
