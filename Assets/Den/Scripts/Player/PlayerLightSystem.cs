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

    public void LightOff()
    {
        // TODO - play light off animation
        lanternLight.enabled = false;
        backLight.enabled = false;
    }

    public void LightOn()
    {
        // TODO - play light off animation
        lanternLight.enabled = true;
        backLight.enabled = true;
    }

    public bool Lighting()
    { 
        return lanternLight.enabled; 
    }
}
