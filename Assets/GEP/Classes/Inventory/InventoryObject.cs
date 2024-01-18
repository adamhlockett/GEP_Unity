using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using Unity.VisualScripting;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/ Inventory")]
public class InventoryObject : ScriptableObject/*, ISerializationCallbackReceiver*/
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;

//    public void OnEnable()
//    {
//#if UNITY_EDITOR
//        database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
//#else 
//        database = Resources.Load<ItemDatabaseObject>("Database");
//#endif
//    }
    public void AddItem(Item item, int amount)
    {
        if (item.buffs != null)
        {
            if (item.buffs.Length > 0)
            {
                SetEmptySlot(item, amount);
                //Container.Items.Add(new InventorySlot(item.ID, item, amount));
                return;
            }
        }

        //foreach (InventorySlot slot in Container.Items)
        if (Container.Items != null && Container.Items.Length > 0)
        {
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].ID == item.ID)
                {
                    Container.Items[i].AddAmt(amount);
                    return;
                }
            }
        }
        SetEmptySlot(item, amount);
        //Container.Items.Add(new InventorySlot(item.ID,item, amount));
    }

    public InventorySlot SetEmptySlot(Item item, int amount)
    {
        if (Container.Items != null && Container.Items.Length > 0)
        {
            for (int i = 0; i < Container.Items.Length; i++)
            {
                if (Container.Items[i].ID <= -1)
                {
                    Container.Items[i].UpdateSlot(item.ID, item, amount);
                    return Container.Items[i];
                }
            }
        }
        //setup what happens when inv is full
        return null;
    }

    //public void OnAfterDeserialize()
    //{
    //    foreach (InventorySlot slot in Container.Items)
    //        slot.item = database.GetItem[slot.ID];
    //    
    //}
    //
    //public void OnBeforeSerialize()
    //{
    //    
    //}

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }

    public void RemoveItem(Item item) 
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()  
    {
        //method allows save data of inventory to be edited by player externally

        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        //method does not allow save data of inventory to be edited by player externally

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath))) 
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Length; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
}

[System.Serializable]
public class Inventory
{
    //public List<InventorySlot> Items = new List<InventorySlot>();
    public InventorySlot[] Items = new InventorySlot[18];
    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(-1, new Item(), 0);
        }
    }
}

[System.Serializable] //show up in editor
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    //[System.NonSerialized]
    public UserInterface parent;
    public int ID = -1;
    public Item item;
    public int amount;

    public InventorySlot()
    {
        this.ID = -1;
        this.item = null;
        this.amount = 0;
    }
    public InventorySlot(int id, Item item, int amount)
    {
        this.ID = id;
        this.item = item;
        this.amount = amount;
    }
    public void UpdateSlot(int id, Item item, int amount)
    {
        this.ID = id;
        this.item = item;
        this.amount = amount;
    }

    public void AddAmt(int value)
    {
        this.amount += value;
    }
    public bool CanPlaceInSlot(ItemObject item)
    {
        if (AllowedItems.Length <= 0) return true;
        for(int i = 0; i < AllowedItems.Length; i++)
        {
            if(item.type == AllowedItems[i]) return true;
        }
        return false;
    }
}
