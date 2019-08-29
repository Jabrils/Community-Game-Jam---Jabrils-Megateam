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

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        if(item == InventoryManager.instance.emptyItem)
        {
            ItemName.gameObject.SetActive(false);
        }
        else
        {
            ItemName.gameObject.SetActive(true);
        }

        if(item == InventoryManager.instance.emptyItem)
        {
            ItemImage.gameObject.SetActive(false);
            ItemImage.sprite = null;
        }
        else
        {
            ItemImage.gameObject.SetActive(true);
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
