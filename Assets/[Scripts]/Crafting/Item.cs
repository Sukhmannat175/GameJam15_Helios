using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SpriteRenderer sr;
    public ItemSO itemSO;

    public void InitilizeItem(ItemSO newItem, int count)
    {
        itemSO = newItem;
        name = itemSO.name;
        sr.sprite = newItem.icon;
    }
}
