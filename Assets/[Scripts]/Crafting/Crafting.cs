using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Crafting : MonoBehaviour
{
    public List<ItemSO> crafter;

    private Item ingredient;

    // Start is called before the first frame update
    void Start()
    {
        ingredient = gameObject.GetComponentInChildren<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            foreach (ItemSO itemSO in ingredient.itemSO.componentIn)
            {
                if (crafter.All(itemSO.recipe.Contains) && crafter.Count == itemSO.recipe.Count) Debug.Log("crafted");
                else Debug.Log("Not Crafted");
            }            
        }
    }
}
