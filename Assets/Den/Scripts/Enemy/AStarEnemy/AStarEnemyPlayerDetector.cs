using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarEnemyPlayerDetector : MonoBehaviour
{
    public bool chase = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if player gets in the range
        if (other.CompareTag("Player"))
        {
            chase = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if player is in the range
        if (other.CompareTag("Player"))
        {
            chase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if player Exit the range
        if (other.CompareTag("Player"))
        {
            chase = false;
        }
    }
}
