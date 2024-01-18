using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public int xStart;
    public int yStart;
    public int xSpace;
    public int ySpace;
    public int columns;
    public GameObject inventoryPrefab;


    public override void CreateSlots()
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

    public Vector3 GetPos(int i)
    {
        return new Vector3((xSpace * (i % columns)), (-ySpace * (i / columns)), 0f);
    }
}
