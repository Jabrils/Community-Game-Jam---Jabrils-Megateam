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
        return MemberwiseClone() as Item;
    }
}
