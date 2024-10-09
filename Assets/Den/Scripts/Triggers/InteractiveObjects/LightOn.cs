using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightOn : MonoBehaviour
{
    [SerializeField]
    private Light2D light;
    [SerializeField]
    private GameObject lightAreaForEnemy;
    private bool trigger = false;

    // Use this for initialization
    void Start()
    {
        light.enabled = false;
    }

    void Update()
    {
        // When the player is closed enough, press e will change the state of light (on -> off; off -> on)
        if (trigger && Input.GetKeyDown(KeyCode.E))
        {
            light.enabled = !light.enabled;

            //// Adjust layers based on light state
            //if (light.enabled)
            //{
            //    // Changing the layer of lightAreaForEnemy to "LightArea"
            //    lightAreaForEnemy.layer = LayerMask.NameToLayer("LightArea");

            //    // Optional: Change specific collision logic if needed using LayerMask
            //    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("LightArea"), LayerMask.NameToLayer("Enemy"), false);
            //}
            //else
            //{
            //    // Changing the layer of lightAreaForEnemy to "Default"
            //    lightAreaForEnemy.layer = LayerMask.NameToLayer("Default");

            //    // Disable player interaction with the light area if light is off
            //    Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("LightArea"), LayerMask.NameToLayer("Enemy"), true);
            //}
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
