using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform UiItemsContainer;
    public List<UiSlotInfo> slots = new List<UiSlotInfo>();
    public int slotAmount = 15;
    public Item testItem;
    public Item emptyItem;

    public static InventoryManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(testItem);
        }
        int input = GetInput();
        if (input != 999)
        {
            AddItemToSlot(testItem, input);
        }
    }

    private int GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            return 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            return 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            return 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            return 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            return 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            return 8;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            return 9;
        }
        return 999;
    }

    public void PickUpItem(ItemInfo item)
    {
        if (AddItem(item.item))
        {
            Destroy(item.gameObject);
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public bool AddItem(Item item)
    {
        if (item.IsStackable)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].item.ItemName == item.ItemName)
                {
                    if (slots[i].item.CurrentStack + item.CurrentStack <= slots[i].item.MaxStack)
                    {
                        slots[i].item.CurrentStack += item.CurrentStack;
                        AddedItem();
                        return true;
                    }
                }
            }
        }

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == emptyItem)
            {
                slots[i].item = item.DeepCopy();
                AddedItem();
                return true;
            }
        }

        return false;
    }

    public bool AddItemToSlot(Item item, int slot)
    {
        if (slot < slots.Count)
        {
            if (item.IsStackable)
            {
                if (slots[slot].item.ItemName == item.ItemName)
                {
                    if (slots[slot].item.CurrentStack + item.CurrentStack <= slots[slot].item.MaxStack)
                    {
                        slots[slot].item.CurrentStack += item.CurrentStack;
                        AddedItem();
                        return true;
                    }
                }
            }

            if (slots[slot].item == emptyItem)
            {
                slots[slot].item = item.DeepCopy();
                AddedItem();
                return true;
            }
        }

        return false;
    }

    public void AddedItem()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].Setup();
        }
    }
    public void GenerateSlots()
    {
        slots.Clear();
        int childAmount = UiItemsContainer.childCount;
        for (int i = 0; i < childAmount; i++)
        {
            DestroyImmediate(UiItemsContainer.GetChild(0).gameObject);
        }
        for (int i = 0; i < slotAmount; i++)
        {
            UiSlotInfo temp = Instantiate(slotPrefab, UiItemsContainer).GetComponent<UiSlotInfo>();
            temp.item = emptyItem;
            temp.Setup();
            slots.Add(temp);
        }
    }
}
