using UnityEngine;

public class Item_Booster : ItemBase
{
    public Shader Shader_Glow;
    //public TrailRenderer BoostTrail;
    //public AudioSource   SFX;
    public float modifySpeed = 0.15f;

    public override void Init(GameObject _Obj)
    {
        _ = _Obj;
        item.itemID = 1004;
        item.itemType = ItemType.REINFORCE;
        item.itemName = "레이더";
        item.itemCount = 0;
        item.itemDesc = "가드가 주변에 있을 때 아웃라인으로 표시됨";
    }
}
