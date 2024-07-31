using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public List<ItemSO> itemDrops;
    public GameObject itemPrefab;

    private SpriteRenderer sr;
    private CircleCollider2D cc;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (sr.sprite == null)
        {
            cc.enabled = false;
            DropItem();
        }
    }

    public void DropItem()
    {
        int rand = Random.Range(0,  itemDrops.Count);

        GameObject obj = Instantiate(itemPrefab, gameObject.transform.position, Quaternion.identity);
        Item objItem = obj.GetComponent<Item>();
        objItem.InitilizeItem(itemDrops[rand], 1);

        this.enabled = false;
    }
}
