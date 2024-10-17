using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangAroundEnemyLightSourceDetector : MonoBehaviour
{
    public bool reachLight = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        // 
        if (other.CompareTag("LightSource"))
        {
            reachLight = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // 
        if (other.CompareTag("LightSource"))
        {
            reachLight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 
        if (other.CompareTag("LightSource"))
        {
            reachLight = false;
        }
    }
}
