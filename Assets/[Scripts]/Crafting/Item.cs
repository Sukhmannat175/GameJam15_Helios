using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    public SpriteRenderer sr;
    public ItemSO itemSO;
    public int count = 1;

    public void InitilizeItem(ItemSO newItem, int count)
    {
        itemSO = newItem;
        name = itemSO.name;
        sr.sprite = newItem.icon;
        this.count = count;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CraftingController.Instance.AddItem(itemSO);
            Destroy(gameObject);
        }
    }
}
