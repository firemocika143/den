using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
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

    public void LightOn()
    {
        //TODO - play animation & set LightArea collider trigger active(hope this would trigger player OncollisionEnter2d but im not sure)
        //TODO - set the detector game object inactive
        //lanternLight2d.enabled = true;
        lightedOn = true;
    }

    public void LightOff() 
    {
        //TODO - play animation & set LightArea collider trigger inactive
        //TODO - set the detector game object active
        //lanternLight2d.enabled = false;
        lightedOn = false;
    }
}
