using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

public class LightTrigger : MonoBehaviour
{
    public bool isShaking {get; private set;}

    [SerializeField]
    private Light2D triggeredLight;
    [SerializeField]
    private float lightOutTime;

    [Header("Animation Settings")]
    [SerializeField]
    private AnimationHandler animHandler;
    [SerializeField]
    private string shakeAnimationName;
    [SerializeField]
    private string waitAnimationName;
    [SerializeField]
    private float shakeAnimationDuration;

    private float timer;

    private void Start()
    {
        ResetTrigger();
    }

    public void Triggered()
    {
        triggeredLight.enabled = true;
        timer = Time.time;
    }

    private IEnumerator ShakeCoroutine(Action after = null)
    {
        isShaking = true;
        animHandler.ChangeAnimationState(shakeAnimationName);
        yield return new WaitForSeconds(shakeAnimationDuration);
        //animation
        animHandler.ChangeAnimationState(waitAnimationName);

        isShaking = false;

        after?.Invoke();
    }

    public void ResetTriggerAfterFailed()
    {
        if (isShaking) return;

        StartCoroutine(ShakeCoroutine(() => { ResetTrigger(); }));
    }

    public void ResetTrigger()
    {
        StopAllCoroutines();

        triggeredLight.enabled = false;
        isShaking = false;
    }

    public void CheckTimeout()
    {
        if (triggeredLight.enabled && Time.time - timer >= lightOutTime)
        {
            ResetTrigger();
        }
    }

    public bool IsTriggered()
    {
        return triggeredLight.enabled;
    }
}
