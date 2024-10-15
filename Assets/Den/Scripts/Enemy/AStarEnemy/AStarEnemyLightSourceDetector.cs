using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarEnemyLightSourceDetector : MonoBehaviour
{
    public bool safe;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if I enter light soruce
        if (other.CompareTag("LightSource"))
        {
            safe = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if I am in the light source
        if (other.CompareTag("LightSource"))
        {
            safe = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if I exit from the light Source
        if (other.CompareTag("LightSource"))
        {
            safe = true;
        }
    }
}
