using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<Item> crafter;

    // Start is called before the first frame update
    void Start()
    {
        //pot = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.C))
        {
            //pot.Sort();
            foreach(Item item in crafter[0].itemSO.componentIn)
            {
                //item.item.recipe.Sort();
                if (crafter.All(item.itemSO.recipe.Contains) && crafter.Count == item.itemSO.recipe.Count) Debug.Log("crafted");
                else Debug.Log("Not Crafted");
            }            
        }
    }
}
