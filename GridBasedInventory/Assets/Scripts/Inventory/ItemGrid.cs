using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGrid : MonoBehaviour
{
    public static float gridTileWidth;
    public static float gridTileHeight;
    public int width;
    public int height;

    RectTransform rectTransform;

    public Transform parent;
    public Transform itemObjParent;
    public GameObject slotPrefab;

    public InventoryItem[,] inventoryItems;

    GridLayoutGroup grid;

    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        gridTileWidth = grid.cellSize.x;
        gridTileHeight = grid.cellSize.y;

        inventoryItems = new InventoryItem[width, height];

        rectTransform = GetComponent<RectTransform>();
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject item = Instantiate(slotPrefab, parent);
            }
        }
    }

    public void PlaceItem(InventoryItem item, Vector2Int gridPos)
    {
        item.SetItem();
        inventoryItems[gridPos.x, gridPos.y] = item;

        for (int y = 0; y < item.itemData.inventoryVolume.y; y++)
        {
            for (int x = 0; x < item.itemData.inventoryVolume.x; x++)
            {
                Vector2Int _position = new Vector2Int(gridPos.x + x, gridPos.y + y);
                inventoryItems[_position.x, _position.y] = item;
            }
        }

        Vector2 position = new Vector2(
            rectTransform.position.x + (gridTileWidth * gridPos.x) + ItemGrid.gridTileWidth / 2,
            rectTransform.position.y - (gridTileHeight * gridPos.y) - ItemGrid.gridTileHeight / 2
        );

        item.GetComponent<RectTransform>().position = position;
        
        item.gameObject.transform.SetParent(itemObjParent);
    }

    public void PickItem(InventoryItem item)
    {
        item.gameObject.transform.SetParent(transform.parent);

        for (int y = 0; y < item.itemData.inventoryVolume.y; y++)
        {
            for (int x = 0; x < item.itemData.inventoryVolume.x; x++)
            {
                Vector2Int _position = new Vector2Int(MousePosToGridPos().x + x, MousePosToGridPos().y + y);
                inventoryItems[_position.x, _position.y] = null;
            }
        }
    }

    public bool isGridAvailable(InventoryItem item, Vector2Int pos)
    {
        bool isCellFull = false;

        for (int y = 0; y < item.itemData.inventoryVolume.y; y++)
        {
            for (int x = 0; x < item.itemData.inventoryVolume.x; x++) 
            {
                Vector2Int position = new Vector2Int(pos.x + x, pos.y + y);
                if (inventoryItems[position.x, position.y] != null) isCellFull = true;
            }
        }

        int right = item.itemData.inventoryVolume.x + pos.x;
        int bottom = item.itemData.inventoryVolume.y + pos.y;

        if (right > width || pos.x < 0 || bottom > height || pos.y < 0 || isCellFull == true) return false;
        else return true;
    }

    public void HighLightCells(Vector2Int volume, Vector2Int origin)
    {
        InventoryManager.instance.currentGrid.DehighlightCells();
        for (int y = 0; y < volume.y; y++)
        {
            for (int x = 0; x < volume.x; x++)
            {
                Vector2Int position = new Vector2Int(origin.x + x, origin.y + y + 1);
                OnHoverHighlight hoverObj = parent.transform.GetChild(GetIndexByGridPos(position)).GetComponent<OnHoverHighlight>();
                hoverObj.Highlight();
            }
        }
    }

    public void DehighlightCells()
    {
        OnHoverHighlight[] objs = parent.GetComponentsInChildren<OnHoverHighlight>();
        foreach (var obj in objs)
        {
            obj.Dehighlight();
        }
    }

    public Vector2Int MousePosToGridPos()
    {
        Vector2 mousePos = InputManager.instance.GetMousePosition();
        Debug.Log(mousePos);

        Vector2 pos = new Vector2(
            (mousePos.x - rectTransform.position.x),
            rectTransform.position.y - mousePos.y
        );

        return new Vector2Int((int)(pos.x / grid.cellSize.x), (int)(pos.y / grid.cellSize.y));
    }

    public int GetIndexByGridPos(Vector2Int position)
    {
        return (position.y - 1) * height + position.x;
    }
}
