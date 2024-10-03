using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightSystem : MonoBehaviour
{
    //connect to PlayerUI
    [SerializeField]
    private Light2D lanternLight;
    [SerializeField]
    private Light2D backLight;
    [SerializeField]
    private AnimationHandler animHandler;

    public void LightOff()
    {
        lanternLight.enabled = false;
        backLight.enabled = false;
    }
    public void IntoLightSourceLightOff()
    {
        lanternLight.enabled = false;

        if (!backLight.enabled) backLight.enabled = true;
    }

    public void LightOn()
    {
        // TODO - play light on animation
        animHandler.ChangeAnimationState("");//enable the turn On light animation play again
        animHandler.ChangeAnimationState("LanternLightOn");
        lanternLight.enabled = true;
        backLight.enabled = true;
    }

    public void LowLightEnergyWarning()
    {
        animHandler.ChangeAnimationState("LanternTwinkle");
    }

    public bool Lighting()
    { 
        return lanternLight.enabled; 
    }
}
