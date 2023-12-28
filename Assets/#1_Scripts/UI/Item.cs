using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    #region Item Info
    public string itemName;
    [TextArea]
    public string itemDescription;
    public Sprite itemImage;
    public GameObject itemPrefab;
    public ItemType itemType;
    public int cost;


    public enum ItemType
    {
        Used,
        Ingredient,
        Potion,
        ETC
    }

    #endregion
}
