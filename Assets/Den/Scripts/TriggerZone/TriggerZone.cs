using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public List<LightTrigger> lightTriggers = new List<LightTrigger>();

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
}
