using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightSystem : MonoBehaviour
{
    public float currentLightRadius { get; private set; }

    [SerializeField]
    private Light2D playerLight;
    [SerializeField]
    private float maxRadius = 6;
    [SerializeField]
    private float onObstacleLightIntensity;

    private float minRadius = 1.5f;
    private float normalUpdateInterval = 0.05f;
    private float fastUpdateInterval = 0.01f;
    private float percentage = 1;
    private float targetPercentage = 1;

    private int currentPlayerMaxE;
    private int currentPlayerE;
    private int addAmount = 1;
    private float origIntensity;
    private float origMaxRadius;

    private Coroutine co_checker = null;

    private void Start()
    {
        origIntensity = playerLight.intensity;
        origMaxRadius = maxRadius;

        StartUpdatePlayerLight();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public bool Lighting()
    {
        return playerLight.enabled;
    }

    public void UpdatePlayerLight(int newLightEnergyAmount, int totalLightEnergyAmount)//, float targetMinPercent
    {
        targetPercentage = (float)newLightEnergyAmount / (float)totalLightEnergyAmount;
        currentPlayerE = newLightEnergyAmount;
        currentPlayerMaxE = totalLightEnergyAmount;
    }

    public void AddMaxRadius(float val)
    {
        maxRadius += val;
    }

    public void ResetMaxRadius()
    {
        maxRadius = origMaxRadius;
    }

    public void StartUpdatePlayerLight()
    {
        if (co_checker != null)
        {
            Debug.LogError("Trying to start more than 1 coroutines at a time");
            return;
        }

        co_checker = StartCoroutine(UpdatePlayerLightRadius());
    }

    private IEnumerator UpdatePlayerLightRadius()
    {
        while (true)
        {
            if (targetPercentage == percentage)
            {
                yield return null;
            }
            else if (targetPercentage > percentage)
            {
                while (percentage < targetPercentage)
                {
                    playerLight.pointLightOuterRadius = Mathf.Lerp(minRadius, maxRadius, percentage);
                    percentage += (float)addAmount / (float) currentPlayerMaxE;

                    float interval = targetPercentage - percentage >= 0.1 ? fastUpdateInterval : normalUpdateInterval;
                    yield return new WaitForSeconds(interval);

                    currentLightRadius = playerLight.pointLightOuterRadius;
                }
            }
            else
            {
                while (percentage > targetPercentage)
                {
                    playerLight.pointLightOuterRadius = Mathf.Lerp(minRadius, maxRadius, percentage);
                    percentage -= (float)addAmount / (float)currentPlayerMaxE;

                    float interval = percentage - targetPercentage >= 0.2 ? fastUpdateInterval : normalUpdateInterval;
                    yield return new WaitForSeconds(interval);

                    currentLightRadius = playerLight.pointLightOuterRadius;
                }
            }
        }
    }

    public void SetIntensity(float intensity)
    {
        playerLight.intensity = intensity;
    }
    public void SetBackIntensity()
    {
        playerLight.intensity = origIntensity;
    }
}

