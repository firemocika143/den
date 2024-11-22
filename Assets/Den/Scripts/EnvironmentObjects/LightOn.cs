using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightOn : MonoBehaviour//LampDevice
{
    [SerializeField]
    private Light2D lanternLight;
    [SerializeField]
    private GameObject lightAreaForEnemy;
    [SerializeField]
    private GameObject lightArea;

    [SerializeField]
    private Material original;
    [SerializeField]
    private Material outline;
    [SerializeField]
    private SpriteRenderer lanternSprite;
    public Charging chargingDevice;

    private bool triggered = false;

    // Use this for initialization
    void Start()
    {
        lanternSprite = GetComponent<SpriteRenderer>();

        lanternLight.enabled = false;
        lightAreaForEnemy.SetActive(false);
        lightArea.SetActive(false);
    }

    //public override void LampStartSetting()
    //{
    //    playerController = FindObjectOfType<PlayerController>();//can this find the script for other objects in the same scene? 
    //    lanternSprite = GetComponent<SpriteRenderer>();
    //    animator = GetComponent<Animator>();
    //    animator.enabled = false;

    //    lanternLight.enabled = false;
    //    lightAreaForEnemy.SetActive(false);
    //    lightArea.SetActive(false);
    //    castTime = 1.5f;
    //}

    //public override void LampUpdate()
    //{
    //    // When the player is closed enough, press e will change the state of light (on -> off; off -> on)
    //    if (playerIsInArea & !lanternLight.enabled && Input.GetKeyDown(GameManager.Instance.keySettings.LightLantern) && playerController.state.lightEnergy > 0)
    //    {
    //        if (!castInProgress)
    //        {
    //            StartCoroutine(Cast());
    //        }
    //    }

    //    if (castRequest)
    //    {
    //        InProgress();

    //        if (Input.GetKeyUp(GameManager.Instance.keySettings.LightLantern) || playerController.state.isDamaged)
    //        {
    //            CastFail();
    //        }
    //    }

    //    if (playerIsInArea && !(lanternLight.enabled || playerController.state.lightEnergy <= 0))
    //    {
    //        lanternSprite.material = outline;
    //    }
    //    else
    //    {
    //        lanternSprite.material = original;
    //    }
    //}

    //public override void ActivatedAbility()
    //{

    //}

    //public override void DeactivatedDevice()
    //{

    //}


    void Update()
    {
        if (triggered && !lanternLight.enabled)
        {
            lanternSprite.material = outline;
        }
        else
        {
            lanternSprite.material = original;
        }
    }

    public void TurnOn()
    {
        lanternSprite.material = original;
        lanternLight.enabled = true;
        lightAreaForEnemy.SetActive(true);
        lightArea.SetActive(true);
    }

    public void TurnOff()
    {
        lanternSprite.material = original;
        lanternLight.enabled = false;
        lightAreaForEnemy.SetActive(false);
        lightArea.SetActive(false);
    }

    public bool LightIsOn()
    {
        return lanternLight.enabled;
    }
    

    public void OnTrigger()
    {
        triggered = true;
    }

    public void OffTrigger()
    {
        triggered = true;
    }
}

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