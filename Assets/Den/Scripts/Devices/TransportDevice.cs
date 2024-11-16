using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TransportDevice : Device
{
    public Transform destination;
    public float deactivateDeviceIntervel;

    private float deactivateCountDown;

    public override void ActivatedAbility()
    {
        PlayerManager.Instance.PlayerTransform().position = destination.position;
        deactivateCountDown = Time.time;
    }

    public override void DeactivatedDevice()
    {
        if (Time.time - deactivateCountDown > deactivateDeviceIntervel)
        {
            DeactivateDeviceReset();
        }
    }
}
