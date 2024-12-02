using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDevice : Device
{
    public GameObject gate;
    public string openGateAnimationName;

    protected override void ActivatedAbility()
    {
        gate.GetComponent<AnimationHandler>().ChangeAnimationState(openGateAnimationName);
        gate.GetComponent<AudioSource>().Play();
    }

    protected override void DeactivatedDevice()
    {

    }

    public override void ShutDownDevice()
    {
        
    }
}
