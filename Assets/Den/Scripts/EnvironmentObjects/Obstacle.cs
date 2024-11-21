using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            HitOnObstacle();
        }
    }

    private void OnCollisionExitr2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            LeaveObstacle();
        }
    }

    private void HitOnObstacle()
    {
        // TODO - enhance volume
        // TODO - reduce player light intensity 
    }

    private void LeaveObstacle()
    {
        // return volume
        // return player light intensity
    }
}
