using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    [System.Serializable]
    public class PlayerState
    {
        [Header("Movement State")]
        public bool stop = false;
        public bool climb = false;
        public bool Hittable;

        //health
        [Header("Health")]
        public int maxHealth = 100;
        public int health;

        //light
        [Header("Light")]
        public int maxLightEnergy = 100;
        public int lightEnergy = 50;

        [Header("Attack")]
        public int attack = 1;
    }

    [System.Serializable]
    public class KeySettings
    {
        [Header("Movement Keys")]
        public KeyCode Left = KeyCode.A;
        public KeyCode Right = KeyCode.D;
        public KeyCode Jump = KeyCode.Space;
        public KeyCode Climb = KeyCode.W;

        [Header("Interaction/Skill Keys")]
        public KeyCode Attack = KeyCode.Mouse0;
        public KeyCode Interaction = KeyCode.E;
        public KeyCode FacePlayerTurnOnLight = KeyCode.S;
    }

    public PlayerState state;
    public KeySettings keySettings;

    //light
    [Header("Other Light Settings")]
    public int lowLight = 5;
    [SerializeField]
    private float gainLightTime = 0.2f;
    [SerializeField]
    private float loseLightTime = 1f;
    public bool isInLightSource = false;

    [Header("Handlers")]
    [SerializeField]
    private PlayerLightSystem lightSystem;
    [SerializeField]
    private PlayerUI playerUI;

    private Coroutine gainLightCoroutine;
    private Coroutine loseLightCoroutine;

    private PlayerMovement playerMovement;
    
    private void Start()
    {
        isInLightSource = false;
        loseLightCoroutine = StartCoroutine(LoseLight());

        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (state.lightEnergy <= 0 && lightSystem.Lighting())
        {
            lightSystem.LightOff();
        }
        else if (state.lightEnergy > 0 && !lightSystem.Lighting() && !isInLightSource)
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

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }

    //Health Function
    public void Damage(int damage)
    {
        state.health -= damage;
        state.health = state.health >= 0 ? state.health : 0;
        playerUI.UpdateHealth(state.health);
    }

    public void Recover(int recover)
    {
        state.health += recover;
        state.health = state.health <= state.maxHealth ? state.health : state.maxHealth;
        playerUI.UpdateHealth(state.health);
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

        lightSystem.IntoLightSourceLightOff();
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

        if (state.lightEnergy > 0)
        {
            lightSystem.LightOn();
        }

        //TODO - show some particles or animations
        
    }

    private IEnumerator GainLight()
    {
        while (true)
        {
            if (state.lightEnergy < state.maxLightEnergy & isInLightSource)
            {
                state.lightEnergy += 1;
                playerUI.UpdateLightEnergy(state.lightEnergy);
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
            if (state.lightEnergy > 0 & !isInLightSource)
            {
                if (state.lightEnergy < lowLight)
                {
                    lightSystem.LowLightEnergyWarning();
                }

                state.lightEnergy -= 1;
                playerUI.UpdateLightEnergy(state.lightEnergy);
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
        this.state.maxHealth = gameData.maxHealth;
        this.state.health = gameData.health;
        this.state.maxLightEnergy = gameData.maxLightEnergy;
        this.state.lightEnergy = gameData.lightEnergy;

        playerUI.UpdateMaxLightEnergy(state.maxLightEnergy);
        playerUI.UpdateMaxHealth(state.maxHealth);
        playerUI.UpdateLightEnergy(state.lightEnergy);
        playerUI.UpdateHealth(state.health);
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.maxHealth = this.state.maxHealth;
        gameData.health = this.state.health;
        gameData.maxLightEnergy = this.state.maxLightEnergy;
        gameData.lightEnergy = this.state.lightEnergy;
    }
}
