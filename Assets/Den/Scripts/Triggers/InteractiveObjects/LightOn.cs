    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightOn : MonoBehaviour
{
	[SerializeField]
    private Light2D light;
    private bool trigger = false;

	// Use this for initialization
	void Start() 
    {
		light.enabled = false;
	}

    void Update()
    {
        // When the player is closed enough, press e will change the state of light (on -> off; off -> on)
        if(trigger && Input.GetKeyDown(KeyCode.E))
        {
            light.enabled = !light.enabled;
        }
    }

	public void OnTrigger()
    {
        trigger = true;
    }

    public void OffTrigger()
    {
        trigger = false;
    }
}
