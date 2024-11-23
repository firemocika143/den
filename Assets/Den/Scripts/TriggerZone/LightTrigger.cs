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

        // animation
        //Vector3 formerPos = transform.parent.position;
        //float amplitude = 0.1f;
        //int sign = -1;
        //float shakeTimer = Time.time;
        //transform.parent.position = new Vector3(formerPos.x - amplitude*sign/2, transform.parent.position.y, 0);
        //while(Time.time - shakeTimer < 0.5f)// I don't think the timer is working correctly
        //{
        //    transform.parent.position = new Vector3(formerPos.x + sign * amplitude, transform.parent.position.y, 0);
        //    sign *= -1;
        //    yield return new WaitForSeconds(0.02f);
        //}
        //transform.parent.position = formerPos;
        animHandler.ChangeAnimationState(shakeAnimationName);
        yield return new WaitForSeconds(shakeAnimationDuration);
        //animation

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
