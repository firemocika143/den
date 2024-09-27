using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerStatus : MonoBehaviour, IDataPersistence
{
    //when should I use m_var like assignment?
    //movement
    [Header("Movement State")]
    public bool stop = false;

    //health
    [Header("Health")]
    public int maxHealth = 100;
    private int health;

    //light
    [Header("Light")]
    public int lowLight = 10;
    [SerializeField]
    private int maxLightEnergy = 100;
    [SerializeField]
    private int lightEnergy = 50;
    [SerializeField]
    private float gainLightTime = 0.2f;
    [SerializeField]
    private float loseLightTime = 1f;
    private bool isInLightSource = false;
    

    [Header("Handlers")]
    [SerializeField]
    private PlayerLightSystem lightSystem;

    private Coroutine gainLightCoroutine;
    private Coroutine loseLightCoroutine;
    
    private void Start()
    {
        isInLightSource = false;
        loseLightCoroutine = StartCoroutine(LoseLight());
    }

    private void Update()
    {
        if (lightEnergy <= 0 && lightSystem.Lighting())
        {
            lightSystem.LightOff();
        }
        else if (lightEnergy > 0 && !lightSystem.Lighting() && !isInLightSource)
        {
            lightSystem.LightOn();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("LightSource"))
        {
            GetIntoLightSource();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("LightSource"))
        {
            LeaveLightSource();
        }
    }

    //light functions
    public void GetIntoLightSource()
    {
        //Called in OnCollisionEnter2D when the player lights on this lantern and when they enters the light area of this lantern
        isInLightSource = true;
        //TODO - Call function in playerStatus to start adding their light energy
        if (loseLightCoroutine != null)
        {
            StopCoroutine(loseLightCoroutine);
            loseLightCoroutine = null;
        }
        gainLightCoroutine = StartCoroutine(GainLight());

        lightSystem.LightOff();
        //TODO - show some particles or animations
    }

    public void LeaveLightSource()
    {
        //Called by OnCollisionExit2D when the player exits the light area
        isInLightSource = false;
        //TODO - Call function in playerStatus to stop adding their light energy
        if (gainLightCoroutine != null)
        {
            StopCoroutine(gainLightCoroutine);
            gainLightCoroutine = null;
        }
        loseLightCoroutine = StartCoroutine(LoseLight());

        if (lightEnergy > 0)
        {
            lightSystem.LightOn();
        }

        //TODO - show some particles or animations
        
    }

    private IEnumerator GainLight()
    {
        while (true)
        {
            if (lightEnergy < maxLightEnergy & isInLightSource)
            {
                lightEnergy += 1;
                Debug.Log("Gain" + lightEnergy);
                yield return new WaitForSeconds(gainLightTime);
            }
            else
            {
                yield return null;
            }
            
        }
    }

    private IEnumerator LoseLight()
    {
        while (true)
        {
            if (lightEnergy > 0 & !isInLightSource)
            {
                if (lightEnergy < lowLight)
                {
                    Debug.Log("Danger, Low Light Energy");
                }

                lightEnergy -= 1;
                Debug.Log("Lose" + lightEnergy);
                yield return new WaitForSeconds(loseLightTime);
            }
            else
            {
                yield return null;
            }
        }
        
    }

    public void LoadData(GameData gameData)
    {
        this.maxHealth = gameData.maxHealth;
        this.health = gameData.health;
        this.maxLightEnergy = gameData.maxLightEnergy;
        this.lightEnergy = gameData.lightEnergy;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.maxHealth = this.maxHealth;
        gameData.health = this.health;
        gameData.maxLightEnergy = this.maxLightEnergy;
        gameData.lightEnergy = this.lightEnergy;
    }
}
