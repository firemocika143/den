using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerStatus : MonoBehaviour
{
    //when should I use m_var like assignment?
    //movement
    public bool stop = false;

    //health
    private int health;

    //light
    [SerializeField]
    private int lightEnergy = 25;
    [SerializeField]
    private float gainLightTime = 1f;
    private bool isInLightSource = false;
    

    [SerializeField]
    private PlayerLightSystem lightSys;
    

    private void Update()
    {
        if (lightEnergy <= 0 && lightSys.Lighting())
        {
            lightSys.LightOff();
        }
        else if (lightEnergy > 0 && !lightSys.Lighting() && !isInLightSource)
        {
            lightSys.LightOn();
        }

    }
    
    //light functions
    public void GetIntoLightSource()
    {
        isInLightSource = true;
        StartCoroutine(GainLight());
        lightSys.LightOff();
    }

    public void LeaveLightSource()
    {
        isInLightSource = false;
        StopCoroutine(GainLight());
        lightSys.LightOn();
    }

    private IEnumerator GainLight()
    {
        while (true)
        {
            lightEnergy += 1;
            yield return new WaitForSeconds(gainLightTime);
        }
    }

}
