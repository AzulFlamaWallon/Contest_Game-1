﻿using UnityEngine;

public class Item_Lader : ItemBase
{
    public Shader Shader_Glow;
    public float dist;

    void Awake()
    {
        item.itemID = 1001;
        item.itemType = ItemType.REINFORCE;
        item.itemName = "레이더";
        item.itemCount = 0;
        item.itemDesc = "가드가 주변에 있을 때 아웃라인으로 표시됨";
    }

    public override void Init(GameObject _Obj)
    {

    }

    public void Lader()
    {
        CharacterController owner = gameObject.GetComponentInParent<CharacterController>();
        Vector3 ownerpos = owner.m_MyProfile.Current_Pos;

        Collider[] victimCols = Physics.OverlapSphere(ownerpos, dist);

        Vector3 vicPos = Vector3.zero;

        CharacterController victim = null;

        Shader oriShader = null;

        for (ushort idx = 0; idx <= victimCols.Length; idx++)
        {
            if (victimCols[idx].CompareTag("Player"))
            {
                victim = victimCols[idx].GetComponentInParent<CharacterController>();
                oriShader = victim.GetComponent<Renderer>().material.shader;
                if (victim.IsGuard())
                {
                    victim.GetComponent<Renderer>().material.shader = Shader.Find(Shader_Glow.name);
                    vicPos = victim.m_MyProfile.Current_Pos;
                }
            }
        }

        if (Vector3.Distance(ownerpos, vicPos) > dist)
        {
            victim.GetComponent<Renderer>().material.shader = Shader.Find(oriShader.name);
        }



    }
}
