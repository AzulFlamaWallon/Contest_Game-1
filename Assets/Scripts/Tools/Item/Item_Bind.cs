using System.Collections;
using UnityEngine;

public class Item_Bind : ItemBase
{
    //public AudioSource   SFX;
    public float duration = 8.0f;

    void Awake()
    {
        item.itemID = 2001;
        item.itemType = ItemType.TRAP;
        item.itemName = "바인드";
        item.itemCount = 0;
        item.itemDesc = "8초간 발이 묶임";
    }
    public override void Init(GameObject _Obj)
    {
        CharacterController owner = _Obj.GetComponentInParent<CharacterController>();
        StartCoroutine(RecoverySpeed(owner));
    }

    IEnumerator RecoverySpeed(CharacterController _Owner)
    {
        float oriSpeed = _Owner.moveSpeed;
        _Owner.moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        _Owner.moveSpeed = oriSpeed;
        yield return null;
    }
}
