using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();
    public GameObject itemPrefab;

    public void AddItem(ItemSO itemSO)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Item item = slots[i].GetComponentInChildren<Item>();

            if(item != null &&
                item.item == itemSO &&
                item.count < 6)
            {
                item.count++;
                item.countText.text = item.count.ToString();
                return;
            }

            if (item == null) 
            {
                GameObject obj = Instantiate(itemPrefab, slots[i].transform);
                obj.GetComponent<Item>().InitilizeItem(itemSO);
                return;
            }
        }
    }
}
