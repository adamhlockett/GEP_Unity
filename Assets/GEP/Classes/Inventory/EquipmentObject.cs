using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{
    public void Awake()
    {
        switch (this.name)
        {
            case "Helmet":
                type = ItemType.Helmet; break;
            case "Shield":
                type = ItemType.Shield; break;
            case "Chest":
                type = ItemType.Chest; break;
            case "Chestplate":
                type = ItemType.Chest; break;
            case "Sword":
                type = ItemType.Weapon; break;
            case "Hammer":
                type = ItemType.Weapon; break;
            case "Boots":
                type = ItemType.Boots; break;
            case "Apple":
                type = ItemType.Food; break;
            case "Bones":
                type = ItemType.Default; break;
            default:
                type = ItemType.Weapon; break;
        }
    }
}
