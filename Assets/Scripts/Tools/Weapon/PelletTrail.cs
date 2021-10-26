using System;
using UnityEngine;

[Serializable]
public class PelletTrail : TrailBase
{
    public Pellet pellet;

    public override void UpdateTrailPositon()
    {
        this.speed = pellet.pelletSpeed;
        base.UpdateTrailPositon();
    }
}