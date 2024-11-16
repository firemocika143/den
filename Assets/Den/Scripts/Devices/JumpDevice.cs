using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDevice : Device
{
    
    public  float countInterval = 2f;
    public float force = 30f;
    public float deactivateDeviceIntervel;

    private float countDown;
    private float deactivateCountDown;

    public override void ActivatedAbility()
    {
        PlayerManager.Instance.GivePlayerForce(new Vector2(0, force));
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
