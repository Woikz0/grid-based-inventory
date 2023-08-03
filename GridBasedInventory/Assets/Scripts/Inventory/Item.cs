using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string Name;
    public Vector2Int inventoryVolume;
    public Sprite ItemSprite;
}
