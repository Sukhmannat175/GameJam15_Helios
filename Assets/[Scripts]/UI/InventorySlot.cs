using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            Item inventoryItem = eventData.pointerDrag.GetComponent<Item>();
            if (inventoryItem != null) inventoryItem.parent = transform;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.red;
    }
}
