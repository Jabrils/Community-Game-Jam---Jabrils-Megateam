using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Camera Cam;
    public GameObject slotPrefab;
    public Transform UiItemsContainer;
    public List<UiSlotInfo> slots = new List<UiSlotInfo>();
    public int slotAmount = 15;
    public Item testItem;
    public Item emptyItem;
    public RectTransform ItemSelection;
    public UiSlotInfo selectedSlot;
    public Canvas canvas;

    public static InventoryManager instance;

    private bool startedDragging;
    private bool onDragOnce;
    private GameObject draggedItemObject;
    private Item clickedItem;
    private UiSlotInfo clickedSlot;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ItemSelection.position = slots[0].GetComponent<RectTransform>().position;
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
            if (input < slots.Count)
            {
                ItemSelection.position = slots[input].GetComponent<RectTransform>().position;
                selectedSlot = slots[input];
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            /*if(Physics.Raycast(Cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                if(hit.transform.GetComponent<ItemInfo>())
                {
                    PickUpItem(hit.transform.GetComponent<ItemInfo>());
                }
                print(hit.transform.name);
            }*/

            if(!startedDragging)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    RectTransform slotTransform = slots[i].GetComponent<RectTransform>();
                    if (isInsideBox(slotTransform.position, slotTransform.sizeDelta * canvas.scaleFactor, Input.mousePosition))
                    {
                        if (slots[i].item != emptyItem)
                        {
                            startedDragging = true;

                            clickedItem = slots[i].item.DeepCopy();
                            clickedSlot = slots[i];
                            slots[i].item = emptyItem;
                        }
                    }
                }
            }
            else
            {
                OnDrop();
                startedDragging = false;
                onDragOnce = false;
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            if (!startedDragging)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    RectTransform slotTransform = slots[i].GetComponent<RectTransform>();
                    if (isInsideBox(slotTransform.position, slotTransform.sizeDelta * canvas.scaleFactor, Input.mousePosition))
                    {
                        if (slots[i].item != emptyItem)
                        {
                            startedDragging = true;

                            clickedItem = slots[i].item.DeepCopy();
                            clickedSlot = slots[i];
                            clickedItem.CurrentStack = 1;

                            if(slots[i].item.CurrentStack > 1)
                            {
                                slots[i].item.CurrentStack--;
                            }
                            else
                            {
                                slots[i].item = emptyItem;
                            }
                        }
                    }
                }
            }
            else
            {
                OnDrop();
                startedDragging = false;
                onDragOnce = false;
            }
        }

        if (startedDragging)
        {
            OnDrag();
        }
    }

    void OnDrag()
    {
        if (!onDragOnce)
        {
            draggedItemObject = new GameObject("DraggedItem");
            draggedItemObject.transform.SetParent(canvas.transform);

            draggedItemObject.AddComponent<Image>().sprite = clickedItem.ItemImage;
            draggedItemObject.GetComponent<RectTransform>().sizeDelta = new Vector2(125, 125) * canvas.scaleFactor;
            AddedItem();
            onDragOnce = true;
        }
        draggedItemObject.transform.position = Input.mousePosition;
    }

    void OnDrop()
    {
        Destroy(draggedItemObject);
        bool found = false;
        for (int i = 0; i < slots.Count; i++)
        {
            RectTransform slotTransform = slots[i].GetComponent<RectTransform>();
            if (isInsideBox(slotTransform.position, slotTransform.sizeDelta * canvas.scaleFactor, Input.mousePosition))
            {
                startedDragging = true;
                if (AddItemToSlot(clickedItem, slots[i]))
                {
                    print("Added");
                }
                else
                {
                    AddItemToSlot(clickedItem, clickedSlot);
                }
                found = true;
            }
        }
        if(!found)
        {
            AddItemToSlot(clickedItem, clickedSlot);
        }
    }

    public static bool isInsideBox(Vector2 position, Vector2 size, Vector2 input)
    {
        if ((position.x + size.x / 2) > input.x && (position.x - size.x / 2) < input.x)
        {
            if ((position.y + size.y / 2) > input.y && (position.y - size.y / 2) < input.y)
            {
                return true;
            }
        }
        return false;
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

    public bool AddItemToSlot(Item item, UiSlotInfo slot)
    {
        if (item.IsStackable)
        {
            if (slot.item.ItemName == item.ItemName)
            {
                if (slot.item.CurrentStack + item.CurrentStack <= slot.item.MaxStack)
                {
                    slot.item.CurrentStack += item.CurrentStack;
                    AddedItem();
                    return true;
                }
            }
        }

        if (slot.item == emptyItem)
        {
            slot.item = item.DeepCopy();
            AddedItem();
            return true;
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
