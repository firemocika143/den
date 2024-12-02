using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnDevice : Device
{
    public float countInterval = 2f;
    public float deactivateDeviceIntervel;
    public LightOn lantern;
    public Animator animator;
    public bool selfDeactivate = true;

    private float countDown;
    private float deactivateCountDown;
    private bool isTriggered = false;
    private bool deactivatable = false;

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
        if (selfDeactivate) deactivateCountDown = Time.time;
    }

    protected override void DeactivatedDevice()
    {
        if (selfDeactivate && Time.time - deactivateCountDown > deactivateDeviceIntervel)
        {
            DeactivateDeviceReset();
        }
        else if (!selfDeactivate)
        {
            if (deactivatable && Time.time - deactivateCountDown > deactivateDeviceIntervel)
            {
                DeactivateDeviceReset();
                deactivatable = false;
            }
        }
    }

    public override void ShutDownDevice()
    {
        deactivateCountDown = Time.time;
        animator.SetBool("trigger", false);
        isTriggered = false;
        lantern.TurnOff();
        deactivatable = true;
        //ResetCharge();
    }
}
