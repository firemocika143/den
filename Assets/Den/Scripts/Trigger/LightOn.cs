using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightOn : MonoBehaviour
{
	public GameObject light;

	// Use this for initialization
	void Start() {
		light.GetComponent<Light2D>().enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
        light.GetComponent<Light2D>().enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        light.GetComponent<Light2D>().enabled = false;
    }
}
