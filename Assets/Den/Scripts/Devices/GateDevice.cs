using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDevice : Device
{
    public GameObject gate;
    public string openGateAnimationName;

    public override void ActivatedAbility()
    {
        gate.GetComponent<AnimationHandler>().ChangeAnimationState(openGateAnimationName);
        gate.GetComponent<AudioSource>().Play();
    }

    public override void DeactivatedDevice()
    {

    }
}
