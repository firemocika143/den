using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightOn : MonoBehaviour
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

    private bool trigger = false;
    private bool castRequest, castSuccess, castInProgress;
    public float castTime;
    private float castStartTime;
    private PlayerController playerController;
    private SpriteRenderer lanternSprite;

    // Use this for initialization
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();//can this find the script for other objects in the same scene? 
        lanternSprite = GetComponent<SpriteRenderer>();

        lanternLight.enabled = false;
        lightAreaForEnemy.SetActive(false);
        lightArea.SetActive(false);
        castTime = 2f;
    }

    void Update()
    {
        // When the player is closed enough, press e will change the state of light (on -> off; off -> on)
        if (trigger & !lanternLight.enabled && Input.GetKeyDown(KeyCode.E) && playerController.state.lightEnergy > 0)
        {
            if (!castInProgress)
            {
                StartCoroutine(Cast());
            }
        }

        if (castRequest)
        {
            InProgress();

            if (Input.GetKeyUp(KeyCode.E) || playerController.state.isDamaged)
            {   
                CastFail();
            }
            else
            {
                if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.E))
                {
                    CastFail();
                }
            }
        }
        
        if (trigger && !(lanternLight.enabled || playerController.state.lightEnergy <= 0))
        {
            lanternSprite.material = outline;
        }
        else
        {
            lanternSprite.material = original;
        }
        Debug.Log(playerController.state.isDamaged);
    }

    private IEnumerator Cast()
    {
        castInProgress = true;

        RequestCast();
        yield return new WaitUntil (() => castRequest == false);

        if(castSuccess)
        {
            Debug.Log("Cast was successful");
            lanternLight.enabled = true;
            lightAreaForEnemy.SetActive(true);
            lightArea.SetActive(true);
        } else
        {
            Debug.Log("Cast was fail");
        }

        castInProgress = false;
    }

    private void RequestCast()
    {
        castRequest = true;
        castSuccess = false;
        castStartTime = Time.time;
        Invoke(nameof(CastSuccess), castTime);
    }

    private void CastSuccess()
    {
        Debug.Log("Invoke");
        castRequest = false;
        castSuccess = true;
    }

    private void CastFail()
    {
        castRequest = false;
        castSuccess = false;
        CancelInvoke(nameof(CastSuccess));
    }

    private void InProgress()
    {
        float timePassed = Time.time - castStartTime;
        Debug.Log(timePassed);
    }

    public void OnTrigger()
    {
        trigger = true;
        //This function should be called by the trigger of the lantern in the inspector
        //TODO - when the player touched the trigger, and if they still have their light on them, show them there is a lantern here
    }

    public void OffTrigger()
    {
        trigger = false;
        //This function should be called by the trigger of the lantern in the inspector
        //TODO - Stop showing anything to player because they had left the detector
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