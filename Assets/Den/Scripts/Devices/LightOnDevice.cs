using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnDevice : Device
{
    public float countInterval = 2f;
    public float deactivateDeviceIntervel;
    public LightOn lantern;
    public Animator animator;

    private float countDown;
    private float deactivateCountDown;
    private bool isTriggered = false;

    protected override void ActivatedAbility()
    {
        isTriggered = !isTriggered;
        if (isTriggered)
        {
            animator.SetBool("trigger", true);
            lantern.TurnOn();
        } else
        {
            animator.SetBool("trigger", false);
            lantern.TurnOff();
        }
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
        animator.SetBool("trigger", false);
        isTriggered = false;
        lantern.TurnOff();
        ResetCharge();
    }
}
