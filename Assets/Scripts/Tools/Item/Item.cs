using System;
using UnityEngine;
using Network.Data;

[CreateAssetMenu(fileName = "Item", menuName = "GreyZoneTools/Create Item")]
[Serializable]
public class Item : ScriptableObject
{
    public Item_Data item_data;
    public ushort itemID;
    public ItemType itemType;
    public string itemName;
    public string itemDesc;
    public byte itemCount;

    public Item() { }
    public Item(ushort _ID, ItemType _Type, string _Name, string _Desc, byte _Count)
    {
        itemID = _ID;
        itemType = _Type;
        itemName = _Name;
        itemDesc = _Desc;
        itemCount = _Count;       
    }
    
    public void SetItemData(Vector3 _Pos, Vector3 _Rot)
    {
        item_data.Position = _Pos;
        item_data.Rotation = _Rot;
    }
}

[Flags]
public enum ItemType
{
    NONE = 0,
    SYSTEM,
    REINFORCE,
    TRAP,
    SECURITY
}