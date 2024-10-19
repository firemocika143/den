using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public bool solved = false;
    public List<LightTrigger> lightTriggers = new List<LightTrigger>();
    public bool returning = false;

    private void Update()
    {
        if (AllTrigged() && !solved && !returning)
        {
            returning = true;
            foreach (LightTrigger trigger in lightTriggers)
            {
                StartCoroutine(trigger.Shake());
            }
        }
        if (AllWaiting() && returning)
        {
            returning = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            CameraManager.Instance.FixCamera(transform);
            Debug.Log("Player Enters");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            CameraManager.Instance.Follow(col.transform);
        }
    }

    private bool AllTrigged()
    {
        foreach (LightTrigger trigger in lightTriggers)
        {
            if (!trigger.isTriggered) return false;
        }

        return true;
    }

    private bool AllWaiting()
    {
        foreach (LightTrigger trigger in lightTriggers)
        {
            if (trigger.isShaking) return false;
        }

        return true;
    }
}
