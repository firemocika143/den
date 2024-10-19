using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightTrigger : MonoBehaviour
{
    [SerializeField]
    private Light2D triggeredLight;
    [SerializeField]
    private float lightOutTime;

    private float timer;

    private void Start()
    {
        triggeredLight.enabled = false;
    }

    private void Update()
    {
        if (triggeredLight.enabled && Time.time - timer >= lightOutTime)
        {
            triggeredLight.enabled = false;
        }
    }

    public void Triggered()
    {
        triggeredLight.enabled = true;
        timer = Time.time;
    }
}
