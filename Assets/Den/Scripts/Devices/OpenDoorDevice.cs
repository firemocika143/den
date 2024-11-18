using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorDevice : Device
{
    public float countInterval = 2f;
    public Animator animator;
    public float deactivateDeviceIntervel;
    public GameObject door;

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
        BoxCollider2D collider2D = door.GetComponent<BoxCollider2D>();
        if (collider2D != null)
        {
            Debug.Log("Trigger");
            collider2D.enabled = !collider2D.enabled;
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
