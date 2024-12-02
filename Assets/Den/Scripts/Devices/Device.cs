using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

public abstract class Device : MonoBehaviour
{
    [SerializeField]
    private Light2D timeLight;
    [SerializeField]
    private float timeLength;
    [SerializeField]
    private GameObject hintArea;

    public bool charged {get; private set;}
    private float radiusMultiplier = 2f;
    private float percentage = 0;
    private float maxRadius = 0;
    private float extendTime = 0.5f;
    private float maxIntensity = 1f;

    private float interruptMaxIntensity = 12f;
    private bool interrupting = false;
    private float interruptTime = 1f;
    private bool charging = false;
    private PlayerController pc = null;

    [HideInInspector]
    public bool playerIsInArea = false;

    protected abstract void ActivatedAbility();
    protected abstract void DeactivatedDevice();
    public abstract void ShutDownDevice();

    private void Start()
    {
        StartReset();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerIsInArea = true;
            pc = col.gameObject.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (charging)
            {
                InterruptCharging();
            }
            playerIsInArea = false;
            col.gameObject.GetComponent<PlayerController>().LeaveDevice();
            pc = null;
        }
    }

    private void Update()
    {
        UpdateCheck();
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
        pc.MatchDevice(this);
        pc.ChangePlayerUseLightRate(0.1f);
        ResetDevice();
        StartCoroutine(AdjustLightRadius(extendTime, 1, null));
        // Start To shrink the radius
        StartCoroutine(AdjustLightRadius(timeLength, 0, Success));
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
        pc.ChangeBackPlayerUseLightRate();
        charged = true;
        // play success animation
        if (hintArea != null) hintArea.SetActive(false);
        //CameraManager.Instance.SwitchBackToCurrentCamera();
        ActivatedAbility();
    }

    public void InterruptCharging()
    {
        if (interrupting || !charging) return;

        charging = false;
        pc.ChangeBackPlayerUseLightRate();
        StopAllCoroutines();
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
                percentage += ((Time.deltaTime / switchTime) * 2);
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
            timeLight.intensity = Mathf.Lerp(0, interruptMaxIntensity, perc);
            perc -= Time.deltaTime / switchTime;
            yield return null;
        }
        timeLight.intensity = 0;
        interrupting = false;
    }

    public void DeactivateDeviceReset()
    {
        ResetDevice();
        charged = false;
        if (hintArea != null) hintArea.SetActive(true);
    }

    public void StartReset()
    {
        charged = false;
        timeLight.pointLightOuterRadius = 0;
        timeLight.pointLightInnerRadius = 0;
        timeLight.intensity = 0;
        percentage = 0;
        maxRadius = radiusMultiplier * timeLength;
    }

    public void UpdateCheck()
    {
        if (!charged && !interrupting)
        {
            if (playerIsInArea && Input.GetKeyDown(GameManager.Instance.keySettings.LightLantern))
            {
                StartCharging();
            }
            if (charging && (!playerIsInArea || Input.GetKeyUp(GameManager.Instance.keySettings.LightLantern) || PlayerManager.Instance.PlayerLightEnergy() <= 0))
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

        if (hintArea != null)
        {
            if (!hintArea.activeSelf) DeactivatedDevice();
        }
    }

    public void ResetCharge()
    {
        charged = false;
    }
}
