using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image image;
    public TMP_Text countText;

    public ItemSO itemSO;
    public int count = 1;
    public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        name = itemSO.name;
        InitilizeItem(itemSO);
    }

    public void InitilizeItem(ItemSO newItem)
    {
        itemSO = newItem;
        image.sprite = newItem.icon;
        countText.text = count.ToString();
        parent = gameObject.transform.parent;
    }

    public void Recount()
    {
        if(count <= 0)
        {
            Destroy(this.gameObject);
        }
        countText.text = count.ToString();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parent = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parent);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(CraftingController.Instance.AddIngredient(itemSO))
        {
            count--;
            Recount();
        }
        transform.SetParent(parent);
    }
}
