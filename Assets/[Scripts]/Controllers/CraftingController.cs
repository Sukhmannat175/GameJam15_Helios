using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour
{
    public List<InventorySlot> itemSlots = new List<InventorySlot>();
    public List<InventorySlot> ingredientSlots = new List<InventorySlot>();
    public Dictionary<ItemSO, int> ingredients = new Dictionary<ItemSO, int>();
    public List<ItemSO> products = new List<ItemSO>();
    public InventorySlot productSlot;
    public GameObject itemPrefab;
    public Button btnCraft;
    public ShadowMeter shadowMeter;
    public int craftShadow = 10;

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

        DontDestroyOnLoad(gameObject);
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
                obj.GetComponent<ItemUI>().InitilizeItemUI(itemSO, count);
                return;
            }
        }
    }

    public void MergeItems(ItemUI merge, ItemUI item, bool call)
    {
        if (merge.itemSO != null &&
            merge.itemSO == item.itemSO)
        {
            int sum = merge.count + item.count;
            if (call)
            {
                ingredients[merge.itemSO] = merge.count + item.count;
                PreCraft();
            }

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
                objItem.InitilizeItemUI(itemSO, 1);
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
        foreach (KeyValuePair<ItemSO, int> kvp in ingredients)
        {
            Debug.Log(kvp.Key + " " + kvp.Value);
        }

        btnCraft.gameObject.SetActive(false);
        if (productSlot.GetComponentInChildren<ItemUI>() != null)
        {
            Destroy(productSlot.GetComponentInChildren<ItemUI>().gameObject);
        }

        if (ingredients.Count <= 0) return;                

        foreach (Dictionary<ItemSO, int> kv in recipes.Values)
        {
            if (kv.Count != ingredients.Count || !kv.Keys.All(ingredients.Keys.Contains)) continue;

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
    }


    public void ShowProduct()
    {
        GameObject obj = Instantiate(itemPrefab, productSlot.transform);
        ItemUI objItem = obj.GetComponent<ItemUI>();
        objItem.InitilizeItemUI(product, ing1 / rec1);
        objItem.parent = productSlot.transform;
        objItem.image.raycastTarget = false;
        btnCraft.gameObject.SetActive(true);
    }

    public void Craft()
    {
        AddItem(product, ing1 / rec1);
        Destroy(productSlot.GetComponentInChildren<ItemUI>().gameObject);

        if (product.itemType == ItemSO.Type.Tier2) craftShadow = 20;
        if (product.itemType == ItemSO.Type.Tier3) craftShadow = 50;
        if (product.itemType == ItemSO.Type.Tier4) craftShadow = 100;
        if (product.itemType == ItemSO.Type.Tier5)
        {
            craftShadow = 200;
            if (!GameController.Instance.tier5.Contains(product)) GameController.Instance.tier5.Add(product);
        }

        shadowMeter.currentValue += craftShadow * ing1 / rec1 ;

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

        if (GameController.Instance.tier5.Count == 3)
        {
            SceneManager.LoadScene(3);
        }
    }
}
