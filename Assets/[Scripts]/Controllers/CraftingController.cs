using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingController : MonoBehaviour
{
    public List<InventorySlot> itemSlots = new List<InventorySlot>();
    public List<InventorySlot> ingredients = new List<InventorySlot>();
    public InventorySlot product;
    public GameObject itemPrefab;

    //Singleton
    public static CraftingController Instance { get; private set; }

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

    public void AddItem(ItemSO itemSO)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            Item item = itemSlots[i].GetComponentInChildren<Item>();

            if(item != null &&
                item.itemSO == itemSO &&
                item.count < 6)
            {
                item.count++;
                item.Recount();
                return;
            }

            if (item == null) 
            {
                GameObject obj = Instantiate(itemPrefab, itemSlots[i].transform);
                obj.GetComponent<Item>().InitilizeItem(itemSO);
                return;
            }
        }
    }

    public bool AddIngredient(ItemSO itemSO)
    {
        for (int i = 0; i < ingredients.Count; i++)
        {
            Item item = ingredients[i].GetComponentInChildren<Item>();

            if (item != null &&
                item.itemSO == itemSO &&
                item.count < 6)
            {
                item.count++;
                item.Recount();
                return true;
            }

            if (item == null) 
            {
                GameObject obj = Instantiate(itemPrefab, ingredients[i].transform);
                Item objItem = obj.GetComponent<Item>();
                objItem.InitilizeItem(itemSO);
                objItem.parent = ingredients[i].transform;
                return true;
            }
        }

        Debug.Log("Cannot add more of this ingredient");
        return false;
    }
}
