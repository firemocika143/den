using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TeleportDevice : Device
{
    public Transform destination;
    public float deactivateDeviceIntervel;

    private float deactivateCountDown;

    protected override void ActivatedAbility()
    {
        PlayerManager.Instance.PlayerTransform().position = destination.position;
        deactivateCountDown = Time.time;
    }

    protected override void DeactivatedDevice()
    {
        if (Time.time - deactivateCountDown > deactivateDeviceIntervel)
        {
            DeactivateDeviceReset();
        }
    }

    public override void ShutDownDevice()
    {
        
    }
}
