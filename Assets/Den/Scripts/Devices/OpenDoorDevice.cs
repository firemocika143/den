using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorDevice : Device
{
    public float countInterval = 2f;
    public Animator animator;
    public float deactivateDeviceIntervel;

    private float countDown;
    private float deactivateCountDown;
    private bool isOpen = false;

    public override void ActivatedAbility()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            animator.SetBool("trigger", true);
        } else
        {
            animator.SetBool("trigger", false);
        }
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
