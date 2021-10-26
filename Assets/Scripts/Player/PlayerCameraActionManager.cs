using System;
using UnityEngine;

public class PlayerCameraActionManager : SingleToneMonoBehaviour<PlayerCameraActionManager>
{
    [Header("DeathCam")]
    public DeathCam DeathCamAction;

    private void Start() 
    {
    }

    public void StartDeathCamAction()
    {
        DeathCamAction.StartCam();
    }
}