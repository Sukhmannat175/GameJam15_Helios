using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEditor.Progress;

public class CraftingController : MonoBehaviour
{
    public List<InventorySlot> itemSlots = new List<InventorySlot>();
    public List<InventorySlot> ingredientSlots = new List<InventorySlot>();
    public Dictionary<ItemSO, int> ingredients = new Dictionary<ItemSO, int>();
    public List<ItemSO> products = new List<ItemSO>();
    public InventorySlot productSlot;
    public GameObject itemPrefab;
    public GameObject crafter;
    public Button btnCraft;
    public ShadowMeter shadowMeter;

    private Dictionary<ItemSO, Dictionary<ItemSO, int>> recipes;
    private ItemSO product;
    private int maxStack = 6;
    private int ing1 = 0, ing2 = 0, ing3 = 0;
    private int rec1 = 0, rec2 = 0, rec3 = 0;

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
        AddItem(itemSO, 1);
    }

    public void AddItem(ItemSO itemSO, int count)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            ItemUI item = itemSlots[i].GetComponentInChildren<ItemUI>();

            if(item != null &&
                item.itemSO == itemSO &&
                item.count < maxStack)
            {
                item.AddCount(count);
                return;
            }

            if (item == null) 
            {
                GameObject obj = Instantiate(itemPrefab, itemSlots[i].transform);
                obj.GetComponent<ItemUI>().InitilizeItem(itemSO, count);
                return;
            }
        }
    }

    public void MergeItems(ItemUI merge, ItemUI item)
    {
        if (merge.itemSO != null &&
            merge.itemSO == item.itemSO)
        {
            int sum = merge.count + item.count;
            if (sum <= maxStack)
            {
                item.count = merge.count + item.count;
                item.Recount();
                Destroy(merge.gameObject);
                return;
            }
            if (sum > maxStack)
            {
                item.count = maxStack;
                item.Recount();
                AddItem(merge.itemSO, sum - maxStack);
                Destroy(merge.gameObject);
                return;
            }
        }
    }

    public bool AddIngredient(ItemSO itemSO)
    {
        for (int i = 0; i < ingredientSlots.Count; i++)
        {
            ItemUI item = ingredientSlots[i].GetComponentInChildren<ItemUI>();

            if (item != null &&
                item.itemSO == itemSO)
            {
                if (item.count < maxStack)
                {
                    item.AddCount(1);
                    ingredients[itemSO] = item.count;
                    PreCraft();
                    return true;
                }
                if (item.count >= maxStack)
                {
                    Debug.Log("Cannot add more of this ingredient");
                    return false;
                }
            }

            if (item == null) 
            {
                GameObject obj = Instantiate(itemPrefab, ingredientSlots[i].transform);
                ItemUI objItem = obj.GetComponent<ItemUI>();
                objItem.InitilizeItem(itemSO, 1);
                objItem.parent = ingredientSlots[i].transform;

                if(!ingredients.ContainsKey(itemSO)) ingredients.Add(itemSO, 1);
                PreCraft();

                return true;
            }
        }

        Debug.Log("Cannot add more of this ingredient");
        return false;
    }

    public bool RemoveIngredient(ItemSO itemSO)
    {
        for (int i = 0; i < ingredientSlots.Count; i++)
        {
            ItemUI item = ingredientSlots[i].GetComponentInChildren<ItemUI>();

            if (item != null &&
                item.itemSO == itemSO)
            {
                ingredients[itemSO]--;
                if (ingredients[itemSO] == 0) ingredients.Remove(itemSO);
                AddItem(itemSO, 1);
                PreCraft();
                return true;
            }
        }
        return false;
    }

    public void PreCraft()
    {
        if(ingredients.Count > 0) Debug.Log(ingredients.ElementAt(0));
        if (ingredients.Count > 1) Debug.Log(ingredients.ElementAt(1));
        if (ingredients.Count > 2) Debug.Log(ingredients.ElementAt(2));
        btnCraft.gameObject.SetActive(false);
        if (productSlot.GetComponentInChildren<ItemUI>() != null)
        {
            Destroy(productSlot.GetComponentInChildren<ItemUI>().gameObject);
        }

        if (ingredients.Count <= 1) return;
        Dictionary<ItemSO, int> backup = new Dictionary<ItemSO, int>();
                

        foreach (Dictionary<ItemSO, int> kv in recipes.Values)
        {
            if (!kv.Keys.All(ingredients.Keys.Contains) && kv.Count != ingredients.Count) return;

            product = recipes.FirstOrDefault(x => x.Value == kv).Key;

            bool equal = true, prop = false;

            ing1 = ingredients.ElementAt(0).Value;
            rec1 = kv[ingredients.ElementAt(0).Key];
            ing2 = ingredients.ElementAt(1).Value;
            rec2 = kv[ingredients.ElementAt(1).Key];

            if (kv.Count == 3)
            {
                ing3 = ingredients.ElementAt(2).Value;
                rec3 = kv[ingredients.ElementAt(2).Key];

                // Exact Ingredients
                if (ing3 != rec3) equal = false;

                // Proportional Ingredients
                if (ing1 / rec1 == ing2 / rec2 &&
                    ing1 / rec1 == ing3 / rec3)
                { prop = true; }
            }

            // Exact Ingredients
            if (ing1 == rec1 &&
                ing2 == rec2 &&
                equal)
            {
                ShowProduct();
                return;
            }

            // Proportional Ingredients
            if (prop || ing1 / rec1 == ing2 / rec2)
            {
                ShowProduct();
                return;
            }
        }


        /*
        int diff1 = ing1 - rec1;
        int diff2 = ing2 - rec2;
        int diff3 = Int32.MaxValue;
        diff3 = ing3 - rec3;

        Dictionary<ItemSO, int> kv1 = new Dictionary<ItemSO, int>();
        Dictionary<ItemSO, int> kv2 = new Dictionary<ItemSO, int>();
        Dictionary<ItemSO, int> kv3 = new Dictionary<ItemSO, int>();

        // Random Ingredients
        if (ing3 < rec3) rand = false;

        if (rand)
        {
            if (ing3 - rec3 < diff3)
            {
                diff3 = ing3 - rec3;
                kv3 = kv;
            }
        }

        // Random Ingredients
        if (ing1 >= rec1 &&
            ing2 >= rec2 &&
            rand)
        {
            if (ing1 - rec1 < diff1)
            {
                diff1 = ing1 - rec1;
                kv1 = kv;
                Debug.Log("Diff 1");
            }

            if (ing2 - rec2 < diff2)
            {
                diff2 = ing2 - rec2;
                kv2 = kv;
                Debug.Log("Diff 2");
            }

            int diff = Math.Min(diff1, Math.Min(diff2, diff3));
            if (diff == diff1) { ShowProduct(product, kv1); Debug.Log("1" + product.name); return; }
            if (diff == diff2) { ShowProduct(product, kv2); Debug.Log("2" + product.name); return; }
            if (diff == diff3) { ShowProduct(product, kv3); Debug.Log("3" + product.name); return; }
        }
        */
    }


    public void ShowProduct()
    {
        GameObject obj = Instantiate(itemPrefab, productSlot.transform);
        ItemUI objItem = obj.GetComponent<ItemUI>();
        objItem.InitilizeItem(product, ing1 / rec1);
        objItem.parent = productSlot.transform;
        objItem.image.raycastTarget = false;
        btnCraft.gameObject.SetActive(true);
    }

    public void Craft()
    {
        AddItem(product, ing1 / rec1);
        Destroy(productSlot.GetComponentInChildren<ItemUI>().gameObject);
        shadowMeter.currentValue += 10;

        for (int i = 0; i < ingredientSlots.Count; i++)
        {
            ItemUI item = ingredientSlots[i].GetComponentInChildren<ItemUI>();

            if (item != null)
            {
                if (i == 0)
                {
                    ingredients[item.itemSO] -= ing1;
                    if (ingredients[item.itemSO] == 0) ingredients.Remove(item.itemSO);
                    item.SubCount(ing1);
                }
                if (i == 1)
                {
                    ingredients[item.itemSO] -= ing2;
                    if (ingredients[item.itemSO] == 0) ingredients.Remove(item.itemSO);
                    item.SubCount(ing2);
                }
                if (i == 2)
                {
                    ingredients[item.itemSO] -= ing3;
                    if (ingredients[item.itemSO] == 0) ingredients.Remove(item.itemSO);
                    item.SubCount(ing3);
                }
            }
        }
        btnCraft.gameObject.SetActive(false);
    }
}
