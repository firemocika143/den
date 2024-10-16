using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarEnemyDetector : MonoBehaviour
{
    [HideInInspector]
    public bool safe = true;
    [HideInInspector]
    public bool chase = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if I enter light soruce
        if (other.CompareTag("LightSource"))
        {
            safe = false;
        }

        if (other.CompareTag("Player"))
        {
            chase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if I exit from the light Source
        if (other.CompareTag("LightSource"))
        {
            safe = true;
        }
        
        if (other.CompareTag("Player"))
        {
            chase = false;
        }
    }
}
