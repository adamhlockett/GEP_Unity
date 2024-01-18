using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Helmet,
    Weapon,
    Shield,
    Boots,
    Chest,
    Default
}

public enum Attributes
{
    Agility,
    Intellect,
    Stamina,
    Strength
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attribute;
    public int value, min, max;
    public ItemBuff(int min, int max)
    {
        this.min = min;
        this.max = max;
        GenerateValue();
    }
    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}

public abstract class ItemObject : ScriptableObject //base class to derive from
{
    public int ID;
    public Sprite UIDisplay;
    public ItemType type;
    [TextArea(15,20)]
    public string desc;
    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int ID;
    public ItemBuff[] buffs;
    public Item()
    {
        Name = "";
        ID = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.ID;
        //Debug.Log(item.buffs.Length);
        if (item.buffs != null)
        {
            if (item.buffs.Length > 0)
                buffs = new ItemBuff[item.buffs.Length];
            else buffs = new ItemBuff[0];
            for (int i = 0; i < buffs.Length; i++)
            {
                buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
                buffs[i].attribute = item.buffs[i].attribute;
            }
        }
    }
}
