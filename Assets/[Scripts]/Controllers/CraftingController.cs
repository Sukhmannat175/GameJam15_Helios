using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftingController : MonoBehaviour
{
    public List<InventorySlot> itemSlots = new List<InventorySlot>();
    public List<InventorySlot> ingredientSlots = new List<InventorySlot>();
    public Dictionary<ItemSO, int> ingredients = new Dictionary<ItemSO, int>();
    public List<ItemSO> products = new List<ItemSO>();
    public InventorySlot product;
    public GameObject itemPrefab;
    public GameObject crafter;

    private Dictionary<ItemSO, Dictionary<ItemSO, int>> recipes;

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

    private void Start()
    {
        recipes = new Dictionary<ItemSO, Dictionary<ItemSO, int>>();

        foreach (ItemSO itemSO in products)
        {
            Dictionary<ItemSO, int> temp = new Dictionary<ItemSO, int>();
            foreach (ItemSO i in itemSO.recipe)
            {
                if (!temp.ContainsKey(i)) temp.Add(i, 1);
                else temp[i]++;
            }

            recipes.Add(itemSO, temp);
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
                item.AddCount();
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
        for (int i = 0; i < ingredientSlots.Count; i++)
        {
            Item item = ingredientSlots[i].GetComponentInChildren<Item>();

            if (item != null &&
                item.itemSO == itemSO &&
                item.count < 6)
            {
                item.AddCount();
                ingredients[itemSO] = item.count;
                Debug.Log("stacked");
                PreCraft();
                return true;
            }

            if (item == null) 
            {
                GameObject obj = Instantiate(itemPrefab, ingredientSlots[i].transform);
                Item objItem = obj.GetComponent<Item>();
                objItem.InitilizeItem(itemSO);
                objItem.parent = ingredientSlots[i].transform;

                if(!ingredients.ContainsKey(itemSO)) ingredients.Add(itemSO, 1);
                PreCraft();

                return true;
            }
        }

        Debug.Log("Cannot add more of this ingredient");
        return false;
    }

    public void PreCraft()
    {
        if (ingredients.Count <= 0) return;

        Item ingredient = crafter.GetComponentInChildren<Item>();

        foreach(Dictionary<ItemSO, int> kv in recipes.Values)
        {
            if (kv.Count != ingredients.Count) Debug.Log("not crafted");

            Debug.Log(kv.All(ingredients.Contains));
            //if (kv.OrderBy(kvp => kvp.Key).SequenceEqual(ingredients.OrderBy(kvp => kvp.Key))) Debug.Log("Crafted");
        }

        foreach (ItemSO itemSO in ingredient.itemSO.componentIn)
        {
            if (ingredients.Keys.All(itemSO.recipe.Contains) && ingredients.Count == itemSO.recipe.Count)
            {
                GameObject obj = Instantiate(itemPrefab, product.transform);
                Item objItem = obj.GetComponent<Item>();
                objItem.InitilizeItem(itemSO);
                objItem.parent = product.transform;
            }
            else Debug.Log("Not Crafted");
        }
    }

    public void Craft(ItemSO itemSO)
    {
        for (int i = 0; i < ingredientSlots.Count; i++)
        {
            Item item = ingredientSlots[i].GetComponentInChildren<Item>();

            if (item != null)
            {
                item.SubCount();
                ingredients[itemSO] = item.count;
            }
        }
        Debug.Log("crafted");
    }
}
