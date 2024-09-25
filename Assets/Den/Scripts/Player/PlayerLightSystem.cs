using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightSystem : MonoBehaviour
{
    //connect to PlayerUI
    [SerializeField]
    private Light2D lanternLight;

    public void LightOff()
    {
        // TODO - play light off animation
        lanternLight.enabled = false;
    }

    public void LightOn()
    {
        // TODO - play light off animation
        lanternLight.enabled = true;
    }

    public bool Lighting()
    { 
        return lanternLight.enabled; 
    }
}
