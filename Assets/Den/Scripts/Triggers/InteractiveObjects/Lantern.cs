using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    private bool lightedOn = false;
    private float clickTimer = -1f;
    public float clickTime = 3f;
    public bool closeEnough = false;

    [SerializeField]
    private Light2D lanternLight2d;

    private void Start()
    {
        lanternLight2d.enabled = false;
    }

    // Start is called before the first frame update
    public void IntoLightSource()
    {
        //Called by the trigger of lightArea when the player lights on this lantern and when they enters the light area of this lantern
        //Activatable only when this lantern is lightedOn
        //TODO - show some particles or animations
        //TODO - Call function in playerStatus to start adding their light energy
    }

    public void LeaveLightSource()
    {
        //Called by the trigger of lightArea when the player exits the light area
        //Activatable only when this lantern is lightedOn
        //TODO - show some particles or animations
        LightOff();
        //TODO - Call function in playerStatus to stop adding their light energy
    }

    public void IntoDetector()
    {
        closeEnough = true;
        //This function should be called by the trigger of the lantern in the inspector
        //TODO - when the player touched the trigger, and if they still have their light on them, show them there is a lantern here
    }

    public void OutOfDetector()
    {
        closeEnough = false;
        //This function should be called by the trigger of the lantern in the inspector
        //TODO - Stop showing anything to player because they had left the detector
    }

    public void LightOn()
    {
        //TODO - play animation
        lanternLight2d.enabled = true;
    }

    public void LightOff() 
    {
        //TODO - play animation
        lanternLight2d.enabled = false;
    }

    //This function would detect when the player hit on it
    private void OnMouseDown()
    {
        //this function should detect when the player is clicking on the lantern (this is not working correctly currently)
        //To light on this lantern
        if (!lightedOn & closeEnough)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //TODO - call game manager function to fix camera
                clickTimer = Time.time;
                //TODO - stop player movement, or reset timer when the try to light on while leaving the lightable area 
            }

            if (Input.GetMouseButtonUp(0))
            {
                clickTimer = -1f;
                //TODO - call gamenager function to release camera to follow the player
            }

            if (clickTimer != -1f & Time.time - clickTimer >= clickTime)
            {
                LightOn();
                //TODO - call gamenager function to release camera to follow the player
            }
        }
    }
}
