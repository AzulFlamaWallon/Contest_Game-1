using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Greyzone.GUI;

/// <summary>
/// 데스 카메라 클래스입니다.
/// 0912 잘안됐던 이거 고쳐놓자.
/// </summary>
public class DeathCam : MonoBehaviour
{
    public GameObject DeathCamTimeline;
    private PlayableDirector playableDirector;
    public CinemachineVirtualCamera[] VCams = new CinemachineVirtualCamera[4];

    private CinemachineBrain DeathCamBrain;
    public GameObject DeathCamBrainObject;

    private Transform DeadPlayer;

    IEnumerator Start()
    {
        yield return null;
        playableDirector = DeathCamTimeline.GetComponent<PlayableDirector>();
        DeathCamBrain = DeathCamBrainObject.GetComponent<CinemachineBrain>();
    }

    public void FindSetDeathPlayer(Transform _Killer, Transform _Ragdoll)
    {
        BindTarget(0,  _Killer,  _Ragdoll);    //Back
        BindTarget(1,  _Ragdoll, _Killer);    //Front
        BindTarget(2, _Killer,  _Ragdoll);    //Left
        BindTarget(3,  _Ragdoll, _Killer);    //Right
    }

    void BindTarget(byte _CamIndex,  Transform _LookAt,  Transform _Follow)
    {
        VCams[_CamIndex].LookAt = _LookAt;
        VCams[_CamIndex].Follow = _Follow;
    }

    void ActiveDeathCamVirtualCameras()
    {
        for (byte index = 0; index < VCams.Length; index++)
        {
            if (DeathCamBrain != null)
            {
                VCams[index] = DeathCamBrain.ActiveVirtualCamera as CinemachineVirtualCamera;
                VCams[index].VirtualCameraGameObject.SetActive(true);
            }
        }
    }

    public void InvokeDeathCamera(Transform _Killer, Transform _Target)
    {
        gameObject.SetActive(true);
        FindSetDeathPlayer(_Killer, _Target);
        DeadPlayer = _Target;
        DeathCamBrainObject.SetActive(true);
        DeathCamTimeline.SetActive(true);
        StartCam();
    }

    public void StartCam()
    {
        // 켜지면 이 카메라가 발격 (더 좋은 방법 떠오르기전 까진 일단 이렇게...)
        Debug.Log("이쪽으로 갔다");
        ActiveDeathCamVirtualCameras();
        playableDirector.Play();
        TooltipManager.Instance.tooltip_HeadMessage.ShowMessage(MessageStyle.ON_HEAD_MSG, "NO SIGNAL...", DeadPlayer.localPosition);
        StartCoroutine(EndCam());
    }

    IEnumerator EndCam()
    {
        yield return new WaitForSeconds((float)playableDirector.duration);
        playableDirector.Stop();
        TooltipManager.Instance.tooltip_HeadMessage.HideMessage();
        DeathCamBrainObject.SetActive(false);        
        DeathCamTimeline.SetActive(false);
        gameObject.SetActive(false);
        yield return null;
    }
}
