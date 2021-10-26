using UnityEngine;

public class Item_BatteryRecovery : ItemBase
{
    public ushort chargeAmount;

    void Awake()
    {
        item.itemID = 4;
        item.itemType = ItemType.SYSTEM;
        item.itemName = "충전소";
        item.itemCount = 0;
        item.itemDesc = "배터리 충전 오브젝트";
    }

    public override void Init(GameObject _Obj)
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        CharacterController character = other.gameObject.GetComponentInParent<CharacterController>();
        if (character.m_MyProfile.Battery < character.battery)
        {
            character.m_MyProfile.Battery += chargeAmount;
        }
    }
}