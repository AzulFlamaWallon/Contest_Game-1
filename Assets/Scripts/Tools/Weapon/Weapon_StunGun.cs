﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Network.Data;
using UnityEngine.Events;

public class Weapon_StunGun : Tool, I_IK_Shotable
{
    [Header("에임 애니메이션, 총구, 총알발사위치")]
    public AnimationClip animAim;
    public Transform     gunleftGrip;
    public Transform     gunMuzzle;

    [Header("비쥬얼 및 사운드 이펙트")]
    [Tooltip("총구 화염")]
    public GameObject    effect_Fire;
    [Tooltip("발사음")]
    public AudioSource   sfx_Fire;

    [Header("펠릿")]
    [Tooltip("펠릿 정보")]
    public Pellet        pelletInfo;
    [Tooltip("펠릿 트레일 오브젝트")]
    public PelletTrail   pelletTrail;

    [Header("어빌리티")]
    public float         stunDuration = 5.0f;
 
    private Vector3      now_pos;
    public  bool         m_IsDebug = true;
    public  ushort       weapon_uid = 5001;
    private bool         m_ThiefShotAble = true;
    

    public AnimationClip Get_Aim_Anim()
    {
        return animAim;
    }

    public Transform Get_Lefthand_Grip()
    {
        return gunleftGrip;
    }

    public Transform Get_Muzzle()
    {
        return gunMuzzle;
    }

    private void Start()
    {
        if (Manager_Network.Instance != null)
            Manager_Network.Instance.e_RoundStart.AddListener(new UnityAction(RestoreThiefShotAble));
    }
    public void RestoreThiefShotAble()
    {
        m_ThiefShotAble = true;
    }

    public override void onFire(bool _pressed)
    {
        if (_pressed)
            return;

        CharacterController attacker = GetComponentInParent<CharacterController>();

        if (!attacker.IsGuard() && !m_ThiefShotAble)
            return;

        // This Add HitScan Logic
        Vector3 fwdDir = attacker.m_CameraAxis.forward;

        Ray ray = new Ray(attacker.m_CameraAxis.position + fwdDir * 2f, fwdDir);
        List<UInt16> victim_IDs = new List<UInt16>();
        List<Vector3> impact_Pos = new List<Vector3>();
        RaycastHit hit;

        sfx_Fire.PlayOneShot(sfx_Fire.clip); // Play Sound

        victim_IDs.Add(0);

        pelletTrail.pellet = pelletInfo; // 펠릿트레일의 펠릿에 커스텀한 펠릿정보를 보내주자.

        for (byte i = 0; i < pelletTrail.pellet.pelletCount; i++)
        {
            GameObject trailobj = Instantiate(pelletTrail.gameObject, now_pos, attacker.m_ToolAxis.rotation);

            if (Physics.Raycast(ray, out hit, pelletTrail.pellet.pelletDist))
            {
                trailobj.GetComponent<PelletTrail>().rayPositon = hit.point;
                CharacterController victim = hit.collider.gameObject.GetComponent<CharacterController>();
                if(victim != null)
                    victim_IDs[i] = victim.m_MyProfile.Session_ID;
                impact_Pos.Add(hit.point);

                //if (victim.IsAlive()) // 네트워크 준비 (피해자가 살아있을때)
                //    StartCoroutine(StunPlayer(victim, stunDuration));
            }
            else
                impact_Pos.Add(ray.GetPoint(pelletTrail.pellet.pelletDist));

            trailobj.GetComponent<PelletTrail>().rayPositon = ray.GetPoint(pelletTrail.pellet.pelletDist);

            DrawFireEffect(attacker, 0.2f);
        }

        SendShotResult(attacker, victim_IDs, impact_Pos);

        //if (!m_IsDebug)
        //{
        //    if (!attacker.IsGuard() && m_ThiefShotAble) // 샷 제한
        //    {
        //        m_ThiefShotAble = false;
        //        // 시각적으로 뭔갈 띄워보면 좋을거같다.
        //    }
        //}
    }

    #region 코루틴
    IEnumerator StunPlayer(CharacterController _Victim, float _StunDuration)
    {
        yield return new WaitForSeconds(0.2f);
        // Need Stun Logic
        _Victim.enabled = false;
        yield return new WaitForSeconds(_StunDuration);
        _Victim.enabled = true;
        // Nee End Stun Logic
        yield return null;
    }
    #endregion

    /// <summary>
    /// 이펙트를 그려줍니다.
    /// </summary>
    /// <param name="_DestroyDur"></param>
    void DrawFireEffect(CharacterController _Attacker, float _DestroyDur)
    {
        GameObject effect = Instantiate(effect_Fire);
        effect.transform.SetParent(_Attacker.m_ToolAxis); // 총구에 이펙트 붙이기
        effect.transform.SetPositionAndRotation(gunMuzzle.position, _Attacker.m_ToolAxis.rotation);
        Destroy(effect, _DestroyDur);
    }

    void SendShotResult(CharacterController _Attacker, List<UInt16> _VictimSecIDs, List<Vector3> _ImpactPos)
    {
        if (Manager_Ingame.Instance.m_Client_Profile.Session_ID == _Attacker.m_MyProfile.Session_ID)
        {
            if (Manager_Network.Instance != null)
            {
                Packet_Sender.Send_Shot_Fire((UInt64)PROTOCOL.MNG_INGAME
                    | (UInt64)PROTOCOL_INGAME.SHOT | (UInt64)PROTOCOL_INGAME.SHOT_FIRE,
                    _VictimSecIDs, _ImpactPos);
            }
        }
    }

    public override void onInteract(bool _pressed)
    {
        
    }

    void FixedUpdate()
    {
        now_pos = gunMuzzle.transform.position;
        //Debug.Log("현재 총구 실시간 위치 : " +  now_pos);
    }
}