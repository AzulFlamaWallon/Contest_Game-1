using System.Linq;
using UnityEngine;

public class Item_AntiTrap : ItemBase
{
    public bool IsUseRange = false;
    public float Range;

    public void Awake()
    {
        item.itemID = 1002;
        item.itemType = ItemType.REINFORCE;
        item.itemName = "안티";
        item.itemCount = 0;
        item.itemDesc = "함정 무력화";
    }

    public override void Init(GameObject _Obj)
    {
        
    }

    public override void onInteract(bool _pressed)
    {
        if (!_pressed)
        {
            return;
        }

        Vector3 ownerpos = GetComponentInParent<GameObject>().transform.position;

        if (IsUseRange)
        {
            Collider[] colliders = Physics.OverlapSphere(ownerpos, Range);

            Collider collider;
            if (colliders.Any((x) => x.GetComponent<Item>().itemType == ItemType.TRAP))
            {
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].CompareTag("Trap"))
                    {
                        // 함정이 무력화 되었다는 문구 띄워보면 어떨까..
                        Destroy(colliders[i]);
                    }
                }
            }
        }
        else
        {
            

        }//Physics.OverlapSphere(, Range).Any((x) => x.CompareTag("Trap"))
    }

}