using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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
    }

    public void InitilizeItem(ItemSO newItem, int count)
    {
        itemSO = newItem;
        this.count = count;
        image.sprite = newItem.icon;
        countText.text = count.ToString();
        parent = gameObject.transform.parent;
    }

    public void AddCount(int num)
    {
        count += num;
        Recount();
    }

    public void SubCount(int num)
    {
        count -= num;
        Recount();        
    }

    public void Recount()
    {
        countText.text = count.ToString();
        if (count > 6)
        {
            CraftingController.Instance.AddItem(itemSO, count - 6);
            this.count = 6;
        }
        if (count <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (transform.parent.CompareTag("ProductSlot")) return;

        image.raycastTarget = false;
        if (transform.parent.CompareTag("IngredientSlot"))
        {
            if (CraftingController.Instance.ingredients.ContainsKey(itemSO)) CraftingController.Instance.ingredients.Remove(itemSO);
            parent = transform.parent;
            transform.SetParent(transform.root);
            CraftingController.Instance.PreCraft();
            return;
        }
        
        parent = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (transform.parent.CompareTag("ProductSlot")) return;

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parent);
        if (transform.parent.CompareTag("IngredientSlot"))
        {
            if (!CraftingController.Instance.ingredients.ContainsKey(itemSO)) CraftingController.Instance.ingredients.Add(itemSO, count);
            CraftingController.Instance.PreCraft();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (transform.parent.CompareTag("ProductSlot")) return;

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (CraftingController.Instance.AddIngredient(itemSO))
            {
                SubCount(1);
            }
            transform.SetParent(parent);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (CraftingController.Instance.RemoveIngredient(itemSO))
            {
                SubCount(1);
            }
            transform.SetParent(parent);
        }
    }
}
