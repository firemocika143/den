using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    private AnimationHandler animHandler;

    public Light2D light2D;
    [SerializeField]
    private int origIntensity;

    public void Off()
    {
        animHandler.ChangeAnimationState("Off");
    }

    public void TurnOffLight()
    {
        light2D.intensity = 0;
    }

    public void LightUp()
    {
        animHandler.ChangeAnimationState("LightUp");
        light2D.intensity = origIntensity;
    }
}
