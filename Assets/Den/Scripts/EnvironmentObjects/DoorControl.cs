using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    public Charging chargingDevice;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (chargingDevice.trigger)
        {
            animator.SetBool("trigger", true);
        }
        else
        {
            animator.SetBool("trigger", false);
        }
    }
}
