using System;
using UnityEngine;

[Serializable]
public class LaserTrail : TrailBase
{
    public Laser Laser;

    public override void UpdateTrailPositon()
    {
        this.speed = Laser.LaserSpeed;
        base.UpdateTrailPositon();
    }
}