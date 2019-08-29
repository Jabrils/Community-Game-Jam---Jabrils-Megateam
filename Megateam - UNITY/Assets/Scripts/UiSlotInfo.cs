using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiSlotInfo : MonoBehaviour
{
    public Image ItemImage;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemStack;
    public Item item;

    public void Setup()
    {
        if(!item.ItemImage)
        {
            ItemImage.sprite = null;
        }
        else
        {
            ItemImage.sprite = item.ItemImage;
        }
        ItemName.text = item.ItemName;
        if(item.IsStackable)
        {
            ItemStack.gameObject.SetActive(true);
            ItemStack.text = item.CurrentStack.ToString() + "x";
        }
        else
        {
            ItemStack.gameObject.SetActive(false);
        }
    }
}
