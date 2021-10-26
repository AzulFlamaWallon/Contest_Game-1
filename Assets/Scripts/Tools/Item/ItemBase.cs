using UnityEngine;

public class ItemBase : Tool
{
    public Item item;

    public override void onFire(bool _pressed)
    {
        if (!_pressed)
            return;
    }

    public override void onInteract(bool _pressed)
    {
        if (!_pressed)
            return;
    }

    public virtual void Init(GameObject _Obj)
    {

    }
}
