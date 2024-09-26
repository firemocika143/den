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
    [SerializeField]
    private int maxLightEnergy = 100;
    [SerializeField]
    private int lightEnergy = 50;
    [SerializeField]
    private float gainLightTime = 0.2f;
    private bool isInLightSource = false;

    [Header("Handlers")]
    [SerializeField]
    private PlayerLightSystem lightSystem;
    

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
    
    //light functions
    public void GetIntoLightSource()
    {
        isInLightSource = true;
        StartCoroutine(GainLight());
        lightSystem.LightOff();
    }

    public void LeaveLightSource()
    {
        isInLightSource = false;
        StopCoroutine(GainLight());
        lightSystem.LightOn();
    }

    private IEnumerator GainLight()
    {
        while (lightEnergy < maxLightEnergy & isInLightSource)
        {
            lightEnergy += 1;
            yield return new WaitForSeconds(gainLightTime);
        }
    }

    public void LoadData(GameData gameData)
    {

    }

    public void SaveData(ref GameData gameData)
    {

    }
}
