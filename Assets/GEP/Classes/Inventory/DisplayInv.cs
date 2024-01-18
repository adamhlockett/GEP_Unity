using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DisplayInv : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public GameObject player;
    public GameObject apple;
    //public InputActionReference playerInput;
    //public PlayerInput pInput;

    public int xStart;
    public int yStart;
    public int xSpace;
    public int ySpace;
    public int columns;
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    //public Vector2 mouseInputPos;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateSlots();
    }
    
    // Update is called once per frame
    void Update()
    {
        //mouseInputPos = playerInput.ReadValue<Vector2>();
        //mouseInputPos = playerInput.action.ReadValue<Vector2>();
        UpdateSlots();
        //UpdateDisplay();
    }

    public void UpdateSlots()
    {
        foreach(KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        //for(int i = 0; i < itemsDisplayed.Count; i++)
        {
            //KeyValuePair<GameObject,InventorySlot> _slot = itemsDisplayed.Keys[i]
            //(_slot.Value.ID == null) return;
            //if (_slot.Value.ID >= 0) // has item
            //{
            //    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.ID].UIDisplay;
            //    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            //    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            //}
            //else
            //{
            //    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            //    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            //    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            //}
            //Debug.Log(_slot.Value.ID);
            
            if (_slot.Value.ID >= 0) // has item
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.ID].UIDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().fontSize = 30;
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().autoSizeTextContainer = false;
            }
            else
            {
                //Debug.Log("1");
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
            
        }
    }
    
    //public void UpdateDisplay()
    //{
    //    for(int i = 0; i < inventory.Container.Items.Count; i++)
    //    {
    //        InventorySlot slot = inventory.Container.Items[i];
    //
    //        if (itemsDisplayed.ContainsKey(slot))
    //        {
    //            itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
    //        }
    //        else
    //        {
    //            Vector3 start_pos = new Vector3(0 + xStart, 0 + yStart, 0);
    //
    //            var obj = Instantiate(inventoryPrefab, /*Vector3.zero*/start_pos, Quaternion.identity, transform);
    //            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.ID].UIDisplay;
    //            obj.GetComponent<RectTransform>().localPosition = GetPos(i);
    //            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");// for
    //            itemsDisplayed.Add(slot, obj);
    //        }
    //    }
    //}
    
    public void CreateSlots()
    {
        //for (int i = 0; i < inventory.Container.Count; i++)
        //{
        //    var obj = Instantiate(inventory.Container[i].);
        //}

        //foreach (InventorySlot slot in inventory.Container)
        //for (int i = 0; i < inventory.Container.Items.Count; i++)
        //{
        //    InventorySlot slot = inventory.Container.Items[i];
        //
        //    //Vector3 start_pos = new Vector3(0 + xStart, 0 + yStart, 0);
        //
        //    var obj = Instantiate(inventoryPrefab, Vector3.zero/*start_pos*/, Quaternion.identity, transform);
        //    obj.GetComponent<RectTransform>().localPosition = GetPos(i);
        //    obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");// format with commas
        //    itemsDisplayed.Add(slot, obj);
        //}

        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPos(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }
    
    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        mouseItem.hoverObj = obj;
        if(itemsDisplayed.ContainsKey(obj))
            mouseItem.hoverItem = itemsDisplayed[obj];
    }
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        //string itemName = "";
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].UIDisplay;
            img.raycastTarget = false;
        }

        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplayed[obj];
    }
    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverObj)
        {
            inventory.MoveItem(itemsDisplayed[obj], itemsDisplayed[mouseItem.hoverObj]);
        }
        else
        {
            string itemName = "";
            itemName = itemsDisplayed[obj].item.Name;
            if (itemName != null)
                Debug.Log(itemName);
            else Debug.Log("null");
            switch (itemName)
            {
                case "Apple":
                    Instantiate(apple, player.transform.position, Quaternion.identity);
                    
                    break;
                default:
                    Instantiate(apple, player.transform.position, Quaternion.identity);
                    break;
            }
            inventory.RemoveItem(itemsDisplayed[obj].item);
            //drop item on floor
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if(mouseItem.obj != null)
        {
            mouseItem.obj.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue() /*new Vector2(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y); mouseInputPos*/; 
        }
    }

    public Vector3 GetPos(int i)
    {
        return new Vector3((xSpace * (i % columns)), (-ySpace * (i / columns)), 0f);
    }
}
//public class MouseItem
//{
//    public GameObject obj;
//    public InventorySlot item;
//    public InventorySlot hoverItem;
//    public GameObject hoverObj;
//}
