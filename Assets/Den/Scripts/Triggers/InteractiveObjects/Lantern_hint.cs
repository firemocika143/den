using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Lantern_hint : MonoBehaviour
{
    public bool closeEnough = false;

    // private Light2D lanternLight2d;
    [SerializeField]
    private GameObject sign;
    [SerializeField]
    private Material original;
    [SerializeField]
    private Material outline;

    private void Start()
    {
        sign.SetActive(false);
    }

    private void Update()
    {
        if (closeEnough)
        {
            GetComponent<SpriteRenderer>().material = outline;
        }
        else
        {
            GetComponent<SpriteRenderer>().material = original;
        }
    }

    public void IntoDetector()
    {
        sign.SetActive(true);     
        closeEnough = true;
        Debug.Log("Triggered (light)");
        //This function should be called by the trigger of the lantern in the inspector
        //TODO - when the player touched the trigger, and if they still have their light on them, show them there is a lantern here
    }

    public void OutOfDetector()
    {
        sign.SetActive(false);  
        closeEnough = false;
        //This function should be called by the trigger of the lantern in the inspector
        //TODO - Stop showing anything to player because they had left the detector
    }
}
