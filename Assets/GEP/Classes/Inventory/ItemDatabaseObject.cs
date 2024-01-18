using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] Items;

    //public Dictionary<ItemObject, int> GetID = new Dictionary<ItemObject,int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        //GetID = new Dictionary<ItemObject, int>();
        for (int i = 0; i < Items.Length; i++)
        {
            //GetID.Add(Items[i], i);
            Items[i].ID = i;
            GetItem.Add(i, Items[i]);
            Debug.Log(Items[i].ID.ToString());
        }
    }
    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }
}