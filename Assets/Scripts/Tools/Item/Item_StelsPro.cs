using UnityEngine;

public class Item_StelsPro : ItemBase
{
    public Shader Shader_Glow;
    public float dist;

    void Awake()
    {
        item.itemID = 1003;
        item.itemType = ItemType.REINFORCE;
        item.itemName = "스텔스 프로";
        item.itemCount = 0;
        item.itemDesc = "가드로부터 위치 노출 방지";
    }
    public override void Init(GameObject _Obj)
    {

    }
}
