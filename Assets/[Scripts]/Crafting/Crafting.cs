using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public List<Item> pot;
    public GameObject ban;
    public GameObject app;

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
            foreach(Item item in pot[0].item.componentIn)
            {
                item.item.recipe.Sort();
                if (pot.All(item.item.recipe.Contains) && pot.Count == item.item.recipe.Count) Debug.Log("crafted");  
                else Debug.Log("Not Crafted");
            }            
        }

        if(Input.GetKeyUp(KeyCode.B))
        {
            Instantiate(ban);
            this.pot.Add(ban.GetComponent<Item>());
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Instantiate(app);
            this.pot.Add(app.GetComponent<Item>());
        }

    }
}
