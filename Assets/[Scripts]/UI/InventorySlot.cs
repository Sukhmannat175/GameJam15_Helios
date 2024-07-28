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
        ItemUI inventoryItem = eventData.pointerDrag.GetComponent<ItemUI>();

        if (CompareTag("ProductSlot")) return;

        if (inventoryItem != null)
        {
            if (transform.childCount == 0)
            {
                inventoryItem.parent = transform;
            }
            if (transform.childCount > 0 &&
                transform.GetComponentInChildren<ItemUI>().itemSO == inventoryItem.itemSO)
            {
                CraftingController.Instance.MergeItems(inventoryItem, GetComponentInChildren<ItemUI>());
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
}
