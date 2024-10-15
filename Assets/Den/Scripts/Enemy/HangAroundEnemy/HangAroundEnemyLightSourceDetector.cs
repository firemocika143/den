using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangAroundEnemyLightSourceDetector : MonoBehaviour
{
    public bool reachLight = false;
    public int dir = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        // if I reach a light source
        if (other.CompareTag("LightSource"))
        {
            reachLight = true;
            dir = -1;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if I stay with player
        if (other.CompareTag("LightSource"))
        {
            reachLight = true;
            dir = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if I stay with player
        if (other.CompareTag("LightSource"))
        {
            reachLight = false;
            dir = 1;
        }
    }
}
