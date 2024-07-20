using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class Item : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TMP_Text countText;

    [HideInInspector] public ItemSO item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        name = item.name;
        InitilizeItem(item);
    }

    public void InitilizeItem(ItemSO newItem)
    {
        item = newItem;
        image.sprite = newItem.icon;
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
}
