using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Lantern_hint : MonoBehaviour
{
    private bool lightedOn = false;
    public bool closeEnough = false;

    [SerializeField]
    // private Light2D lanternLight2d;
    private GameObject sign;

    private void Start()
    {
        sign.SetActive(false);
    }

    private void Update()
    {
        if (closeEnough)
        {
            /*if (sign.transform.GetChild(0).TryGetComponent<TextMeshPro>(out TextMeshPro text))
                {
                    text.text = "E";
                    text.fontSize = 6;
                }*/
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
