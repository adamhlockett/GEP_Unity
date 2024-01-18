using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class UserInterface : MonoBehaviour
{

    public Player player;
    public GameObject[] groundItems;
    public GameObject itemHolder;

    public InventoryObject inventory;
    private ItemObject data;
    //public InputActionReference playerInput;
    //public PlayerInput pInput;

    public Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    //public Vector2 mouseInputPos;

    void Start()
    {
        //if(inventory.Container.Items != null && inventory.Container.Items.Length > 0)
        //{
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                //Debug.Log(i);
                inventory.Container.Items[i].parent = this; // gets error when not reset
            }
        //}
        
        CreateSlots();
        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
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
        //for(int i = 0; i < itemsDisplayed.Count; i++)
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
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
                
                if (!inventory.database.GetItem.TryGetValue(_slot.Value.item.ID, out data))
                {
                    Debug.LogError("dictionary doesn't have the value");
                }
                else
                {
                    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.ID].UIDisplay;
                }

                //if (inventory.database.GetItem[_slot.Value.item.ID].UIDisplay != null)
                //    _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.ID].UIDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
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

    public abstract void CreateSlots();
    //{
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

        //itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        //for (int i = 0; i < inventory.Container.Items.Length; i++)
        //{
        //    var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
        //    obj.GetComponent<RectTransform>().localPosition = GetPos(i);
        //
        //    AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
        //    AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
        //    AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
        //    AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
        //    AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
        //
        //    itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        //}
    //}

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        player.mouseItem.hoverObj = obj;
        if (itemsDisplayed.ContainsKey(obj))
            player.mouseItem.hoverItem = itemsDisplayed[obj];
    }
    public void OnExit(GameObject obj)
    {
        player.mouseItem.hoverObj = null;
        player.mouseItem.hoverItem = null;
    }
    public void OnEnterInterface(GameObject obj)
    {
        player.mouseItem.ui = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj)
    {
        player.mouseItem.ui = null;
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);
        if (itemsDisplayed[obj].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[obj].ID].UIDisplay;
            img.raycastTarget = false;
        }

        player.mouseItem.obj = mouseObject;
        player.mouseItem.item = itemsDisplayed[obj];
    }
    public void OnDragEnd(GameObject obj)
    {
        var itemOnMouse = player.mouseItem;
        var mouseHoverItem = itemOnMouse.hoverItem;
        var mouseHoverObj = itemOnMouse.hoverObj;
        var getItemObject = inventory.database.GetItem;

        if (itemOnMouse.ui != null)
        {
            if (mouseHoverObj)
                if (mouseHoverItem.CanPlaceInSlot(getItemObject[itemsDisplayed[obj].ID]) && (mouseHoverItem.item.ID <= -1 || (mouseHoverItem.item.ID >= 0 && itemsDisplayed[obj].CanPlaceInSlot(getItemObject[mouseHoverItem.item.ID]))))
                    inventory.MoveItem(itemsDisplayed[obj], mouseHoverItem.parent.itemsDisplayed[itemOnMouse.hoverObj]);
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
                    (Instantiate(groundItems[0], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
                case "Bones":
                    (Instantiate(groundItems[1], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
                case "Boots":
                    (Instantiate(groundItems[2], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
                case "Chestplate":
                    (Instantiate(groundItems[3], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
                case "Hammer":
                    (Instantiate(groundItems[4], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
                case "Helmet":
                    (Instantiate(groundItems[5], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
                case "Shield":
                    (Instantiate(groundItems[6], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
                case "Sword":
                    (Instantiate(groundItems[7], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
                default:
                    (Instantiate(groundItems[0], new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity) as GameObject).transform.parent = itemHolder.transform;
                    break;
            }
            inventory.RemoveItem(itemsDisplayed[obj].item);
            //spawn correstponding item into the world as grounditem
        }
        Destroy(player.mouseItem.obj);
        itemOnMouse.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if (player.mouseItem.obj != null)
        {
            player.mouseItem.obj.GetComponent<RectTransform>().position = new Vector2(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y); //mouseInputPos; 
        }
    }

    
}
public class MouseItem
{
    public UserInterface ui;
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}
