using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

public class Device : MonoBehaviour
{
    [SerializeField]
    private Light2D timeLight;
    [SerializeField]
    private CinemachineVirtualCamera focusCam;
    [SerializeField]
    private float timeLength;
    [SerializeField]
    private GameObject hintArea;

    private bool charged = false;
    private float radiusMultiplier = 4f;
    private bool playerIsInArea = false;
    private float percentage = 0;
    private float maxRadius = 0;
    private float extendTime = 0.5f;
    private float maxIntensity = 1f;
    private bool interrupting = false;
    private float interruptTime = 1f;
    private bool charging = false;

    private void Start()
    {
        timeLight.pointLightOuterRadius = 0;
        timeLight.pointLightInnerRadius = 0;
        timeLight.intensity = 0;
        percentage = 0;
        maxRadius = radiusMultiplier * timeLength;

        focusCam.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsInArea = true;
            Debug.Log("Enter");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsInArea = false;
            Debug.Log("Leave");
        }
    }

    private void Update()
    {
        if (!charged && !interrupting)
        {
            if (playerIsInArea && Input.GetKeyDown(GameManager.Instance.keySettings.LightLantern))
            {
                StartCharging();
            }
            if (charging && (!playerIsInArea || Input.GetKeyUp(GameManager.Instance.keySettings.LightLantern)))
            {
                InterruptCharging();
            }
        }
        else if (!charged && interrupting)
        {
            if (Input.GetKeyDown(GameManager.Instance.keySettings.LightLantern))
            {
                // play uninteractable animation
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void StartCharging()
    {
        if (charging) return;

        StopAllCoroutines();

        charging = true;
        Debug.Log("start charging");
        ResetDevice();
        StartCoroutine(AdjustLightRadius(extendTime, 1, null));
        Debug.Log("Extend");
        // Player Stop losing light
        // Fix Player Camera
        CameraManager.Instance.SwitchOtherCamera(focusCam);
        // Start To shrink the radius
        StartCoroutine(AdjustLightRadius(timeLength, 0, Success));
        Debug.Log("Shrink");
    }

    private void ResetDevice()
    {
        timeLight.pointLightOuterRadius = 0;
        timeLight.intensity = maxIntensity;
        percentage = 0;
    }

    private void Success()
    {
        charging = false;
        charged = true;
        // play success animation
        Debug.Log("success");
        hintArea.SetActive(false);
    }

    private void InterruptCharging()
    {
        if (interrupting) return;

        charging = false;
        Debug.Log("interrupt");
        StopAllCoroutines();
        // return camera
        CameraManager.Instance.SwitchBackToCurrentCamera();
        focusCam.enabled = false;
        // Player Start Losing Light

        StartCoroutine(KilLight(interruptTime));
        
        // maybe I need a player charging manager to be called if the player is damaged or have no light energy left
    }

    private IEnumerator AdjustLightRadius(float switchTime, float targetPercentage = 1, Action action_after = null)
    {
        if (targetPercentage == percentage)
        {
            yield return null;
        }
        else if (targetPercentage > percentage)
        {
            while (percentage < targetPercentage)
            {
                timeLight.pointLightOuterRadius = Mathf.Lerp(0, maxRadius, percentage);
                percentage += Time.deltaTime / switchTime;
                yield return null;
            }
            timeLight.pointLightOuterRadius = maxRadius;
            percentage = targetPercentage;
        }
        else
        {
            while (percentage > targetPercentage)
            {
                timeLight.pointLightOuterRadius = Mathf.Lerp(0, maxRadius, percentage);
                percentage -= Time.deltaTime / switchTime;
                yield return null;
            }
            timeLight.pointLightOuterRadius = maxRadius * targetPercentage;
            percentage = targetPercentage;
        }

        action_after?.Invoke();
    }

    private IEnumerator KilLight(float switchTime, Action action_after = null)
    {
        interrupting = true;
        float perc = 1;

        while (perc > 0)
        {
            timeLight.intensity = Mathf.Lerp(0, maxIntensity, perc);
            perc -= Time.deltaTime / switchTime;
            yield return null;
        }
        timeLight.intensity = 0;
        interrupting = false;
    }
}
