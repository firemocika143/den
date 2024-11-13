using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightSystem : MonoBehaviour
{
    public enum LightState
    {
        NOLIGHTENERGY,
        WITHLIGHTENERGY,
        INLIGHTSOURCE
    }

    [SerializeField]
    private Light2D playerLight;
    //[SerializeField] 
    //private Light2D globalLight;
    
    [SerializeField]
    private float maxRadius;
    [SerializeField]
    private float minRadius;

    //[SerializeField]
    //private float maxIntensity;
    //[SerializeField]
    //private float minIntensity;

    //private float percentage = 0;
    //private float i_percentage = 1;

    private void Start()
    {
        //playerLight.pointLightOuterRadius = 0;
        ////globalLight.enabled = false;
    }

    private void OnDestroy()
    {
        //StopAllCoroutines();
    }

    public bool Lighting()
    {
        return playerLight.enabled;
    }

    public void ChangeLight(LightState l_state)//, float targetMinPercent
    {
        ////if (targetMinPercent < 0 || targetMinPercent > 1)
        ////{
        ////    Debug.LogError("target percent out of range");
        ////    return;
        ////}

        //StopAllCoroutines();

        //switch (l_state)
        //{
        //    //case LightState.NOLIGHTENERGY:
        //    //    StartCoroutine(AdjustLightR(3f, minLightPercentage));
        //    //    break;
                
        //    //case LightState.WITHLIGHTENERGY:
        //    //    StartCoroutine(AdjustLightR(3f, 1));
        //    //    break;

        //    //case LightState.INLIGHTSOURCE:
        //    //    StartCoroutine(AdjustLightR(1f, 0));
        //    //    break;

        //    case LightState.NOLIGHTENERGY:
        //        //globalLight.enabled = false;
        //        StartCoroutine(AdjustLightR(1f, minRadius / maxRadius));
        //        break;

        //    case LightState.WITHLIGHTENERGY:
        //        //globalLight.enabled = false;
        //        StartCoroutine(AdjustLightR(1f, 1));
        //        break;

        //    case LightState.INLIGHTSOURCE:
        //        //globalLight.enabled = true;
        //        StartCoroutine(AdjustLightR(1f, 1));
        //        break;
        //}
    }

    private IEnumerator AdjustLightR(float switchTime, float targetPercentage = 1, Action after_light_adjust = null)
    {
        //if (targetPercentage == percentage)
        //{
        //    yield return null;
        //}
        //else if (targetPercentage > percentage)
        //{
        //    while (percentage < targetPercentage)
        //    {
        //        playerLight.pointLightOuterRadius = Mathf.Lerp(0, maxRadius, percentage);
        //        percentage += Time.deltaTime / switchTime;
        yield return null;
        //    }
        //    playerLight.pointLightOuterRadius = maxRadius;
        //    percentage = targetPercentage;

        //    after_light_adjust?.Invoke();
        //}
        //else
        //{
        //    while (percentage > targetPercentage)
        //    {
        //        playerLight.pointLightOuterRadius = Mathf.Lerp(0, maxRadius, percentage);
        //        percentage -= Time.deltaTime / switchTime;
        //        yield return null;
        //    }
        //    playerLight.pointLightOuterRadius = maxRadius * targetPercentage;
        //    percentage = targetPercentage;

        //    after_light_adjust?.Invoke();
        //}
    }

    //private IEnumerator AdjustLightI(float switchTime, float targetPercentage = 1, Action after_light_adjust = null)
    //{
    //    if (targetPercentage == i_percentage)
    //    {
    //        yield return null;
    //    }
    //    else if (targetPercentage > i_percentage)
    //    {
    //        while (i_percentage < targetPercentage)
    //        {
    //            playerLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, i_percentage);
    //            i_percentage += Time.deltaTime / switchTime;
    //            yield return null;
    //        }
    //        playerLight.intensity = maxIntensity;
    //        i_percentage = targetPercentage;

    //        after_light_adjust?.Invoke();
    //    }
    //    else
    //    {
    //        while (i_percentage > targetPercentage)
    //        {
    //            playerLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, i_percentage);
    //            i_percentage -= Time.deltaTime / switchTime;
    //            yield return null;
    //        }
    //        playerLight.intensity = maxIntensity * targetPercentage;
    //        i_percentage = targetPercentage;

    //        after_light_adjust?.Invoke();
    //    }
    //}
}

