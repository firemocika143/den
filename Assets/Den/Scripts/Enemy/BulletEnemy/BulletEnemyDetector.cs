using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyDetector : MonoBehaviour
{
    public bool safe = true;
    public bool shoot = false;
    public Transform target = null;//you know, the better way may be putting into BulletEnemy Script directly

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if I enter light soruce
        if (other.CompareTag("LightSource"))
        {
            safe = false;
        }

        if (other.CompareTag("Player"))
        {
            shoot = true;
            target = other.transform;
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
            shoot = false;
            target = null;
        }
    }
}
