using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;

    public MouseItem mouseItem = new MouseItem();

    private void Start()
    {
        //inventory.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            //Debug.Log(item.name);
            Debug.Log(item.item);
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if(Keyboard.current.kKey.isPressed)
        {
            inventory.Save();
        }
        if (Keyboard.current.lKey.isPressed)
        {
            inventory.Load();
        }
    }

    private void OnApplicationQuit()
    {
        //inventory.Container.Items = new InventorySlot[18];
        inventory.Clear();
    }
}
