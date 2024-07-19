using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<Item> pot;

    // Start is called before the first frame update
    void Start()
    {
        pot = new List<Item>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.C))
        {
            pot.Sort();
            foreach(Item item in pot[0].item.componentIn)
            {
                item.item.recipe.Sort();
                if (pot.SequenceEqual(item.item.recipe)) Debug.Log("crafted");  
                else Debug.Log("Not Crafted");
            }            
        }

    }
}
