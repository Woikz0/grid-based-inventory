using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
{
    public Item itemData;
    public Color notAvaliableColor;

    Image image;
    RectTransform rectTransform;
    bool isDragging = false;

    public void SetItem()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        image.sprite = itemData.ItemSprite;

        GetComponent<RectTransform>().sizeDelta = new Vector2(itemData.inventoryVolume.x * ItemGrid.gridTileWidth, itemData.inventoryVolume.y * ItemGrid.gridTileHeight);
        Vector2 pivot = new Vector2(
            1f / itemData.inventoryVolume.x / 2f,
            1f - 1f / itemData.inventoryVolume.y / 2f
        );
        GetComponent<RectTransform>().pivot = pivot;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.instance.currentGrid.PickItem(this);
        InventoryManager.instance.lastSelectedItemPos = InventoryManager.instance.currentGrid.MousePosToGridPos();
        isDragging = true;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (isDragging)
        {
            rectTransform.position = InputManager.instance.GetMousePosition();
            bool isGridAvailable = InventoryManager.instance.currentGrid.isGridAvailable(this, InventoryManager.instance.currentGrid.MousePosToGridPos());
            if (!isGridAvailable)
            {
                image.color = notAvaliableColor;
                InventoryManager.instance.currentGrid.HighLightCells(itemData.inventoryVolume, InventoryManager.instance.lastSelectedItemPos);
            }
            else
            {
                image.color = Color.white;
                InventoryManager.instance.currentGrid.HighLightCells(itemData.inventoryVolume, InventoryManager.instance.currentGrid.MousePosToGridPos());
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isGridAvailable = InventoryManager.instance.currentGrid.isGridAvailable(this, InventoryManager.instance.currentGrid.MousePosToGridPos());
        if (isGridAvailable)
        {
            InventoryManager.instance.currentGrid.PlaceItem(this, InventoryManager.instance.currentGrid.MousePosToGridPos());
        }
        else InventoryManager.instance.currentGrid.PlaceItem(this, InventoryManager.instance.lastSelectedItemPos);
        image.color = Color.white;
        isDragging = false;
        InventoryManager.instance.currentGrid.DehighlightCells(); 
    }
}
