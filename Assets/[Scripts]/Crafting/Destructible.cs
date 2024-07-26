using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public List<ItemSO> itemDrops;
    public GameObject itemPrefab;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (sr.sprite == null)
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        int rand = Random.Range(0,  itemDrops.Count - 1);

        GameObject obj = Instantiate(itemPrefab, gameObject.transform);
        Item objItem = obj.GetComponent<Item>();
        objItem.InitilizeItem(itemDrops[rand], 1);

        this.enabled = false;
    }
}
