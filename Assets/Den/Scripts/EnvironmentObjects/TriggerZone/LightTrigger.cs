using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightTrigger : MonoBehaviour
{
    public bool isTriggered = false;
    public bool isShaking;

    [SerializeField]
    private Light2D triggeredLight;
    [SerializeField]
    private float lightOutTime;
    [SerializeField]
    private TriggerZone triggerZone;

    private float timer;

    private void Start()
    {
        triggeredLight.enabled = false;
    }

    private void Update()
    {
        if (triggeredLight.enabled && Time.time - timer >= lightOutTime && !triggerZone.solved)
        {
            triggeredLight.enabled = false;
            isTriggered = false;
        }
    }

    public void Triggered()
    {
        triggeredLight.enabled = true;
        isTriggered = true;
        timer = Time.time;
    }

    public IEnumerator Shake()
    {
        isShaking = true;
        Vector3 formerPos = transform.parent.position;
        float amplitude = 0.1f;
        int sign = -1;
        float shakeTimer = Time.time;
        transform.parent.position = new Vector3(formerPos.x - amplitude*sign/2, transform.parent.position.y, 0);
        while(Time.time - shakeTimer < 0.5f)// I don't think the timer is working correctly
        {
            transform.parent.position = new Vector3(formerPos.x + sign * amplitude, transform.parent.position.y, 0);
            sign *= -1;
            yield return new WaitForSeconds(0.02f);
        }
        transform.parent.position = formerPos;
        isShaking = false;
    }
}
