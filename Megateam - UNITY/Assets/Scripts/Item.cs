using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem",menuName = "Item")]
public class Item : ScriptableObject
{
    public Sprite ItemImage;
    public string ItemName;
    public bool IsStackable;
    public int MaxStack;
    public int CurrentStack = 1;

    public Item DeepCopy()
    {
        Item temp = new Item();
        temp.ItemImage = ItemImage;
        temp.ItemName = ItemName;
        temp.IsStackable = IsStackable;
        temp.MaxStack = MaxStack;
        temp.CurrentStack = CurrentStack;
        return temp;
    }
}
