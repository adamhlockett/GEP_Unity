using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearInv : MonoBehaviour
{
    [SerializeField]
    public InventoryObject inventory;

    private void Start()
    {
        //inventory.Clear();
    }
    void OnApplicationQuit()
    {
        //inventory.Clear();
    }
}
