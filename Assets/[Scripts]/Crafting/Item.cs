using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IComparable, IComparable<Item>
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

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        if (obj is Item other) return CompareTo(other);

        throw new ArgumentException("An Item object is required for comparison.", "obj");
    }

    public int CompareTo(Item other)
    {
        if (other == null) return 1;

        return -string.Compare(this.item.name, other.item.name, StringComparison.OrdinalIgnoreCase);
    }
}
