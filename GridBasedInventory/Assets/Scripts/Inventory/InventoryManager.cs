using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public InventoryItem selectedItem;
    public Vector2Int lastSelectedItemPos;
    public ItemGrid currentGrid;

    private void Awake()
    {
        instance = this;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(1))
        {
            currentGrid.PlaceItem(selectedItem, new Vector2Int(Random.Range(0, 5), Random.Range(0, 5)));
        }
    }

}
