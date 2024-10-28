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

        [Header("Other Player State Settings")]
        public bool isHittable = true;
        public float unhittableTime = 1f;

        //[Header("Skill Obtaining States")]
        //public bool getLightDraw = false;
    }

    public PlayerState state;

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
    
    private float gainLightTimer; 
    private float loseLightTimer;

    private PlayerMovement playerMovement;
    private PlayerUI playerUI;
    private PlayerAttack playerAttack;
    private float unhittableTimer;
    
    private void Start()
    {
        isInLightSource = false;
        gainLightTimer = Time.time;
        loseLightTimer = Time.time;
        //loseLightCoroutine = StartCoroutine(LoseLight());

        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerUI = FindFirstObjectByType<PlayerUI>();//this is not good..., nut how to set UI Manager?
        if (TryGetComponent<LineRenderer>(out var lineRenderer))
        {
            lineRenderer.enabled = false;
        }

        LeaveLightSource();
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

        UpdatePlayerLightEnergy();

        if (!state.isHittable && Time.time - unhittableTimer > state.unhittableTime)
        {
            state.isHittable = true;
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
        //what if we leave one and enter the other one immediately?
        if (col.gameObject.CompareTag("LightSource"))
        {
            LeaveLightSource();
        }
    }

    //Player State Functions
    public void Damage(int damage)
    {
        if (state.isHittable)
        {
            state.health -= damage;
            state.health = state.health >= 0 ? state.health : 0;
            playerUI.UpdateHealth(state.health);
            state.isHittable = false;
            unhittableTimer = Time.time;

            if (state.health <= 0)
            {
                PlayerKilled();
            }
        }
    }

    public void Recover(int recover)
    {
        state.health += recover;
        state.health = state.health <= state.maxHealth ? state.health : state.maxHealth;
        playerUI.UpdateHealth(state.health);
    }

    public void AllRecover()
    {
        state.health = state.maxHealth;
        state.lightEnergy = state.maxLightEnergy;

        if (playerUI == null) return;
        playerUI.UpdateMaxHealth(state.maxHealth);
        playerUI.UpdateHealth(state.health);
        playerUI.UpdateMaxLightEnergy(state.maxLightEnergy);
        playerUI.UpdateLightEnergy(state.lightEnergy);
    }

    public void UseLightEnergy(int val)
    {
        state.lightEnergy = state.lightEnergy - val >= 0 ? state.lightEnergy - val : 0;
        playerUI.UpdateLightEnergy(state.lightEnergy);
    }

    //Events functions
    private void PlayerKilled()
    {
        // TODO - player dying Animation
        GameManager.Instance.PlayerRespawn(this.gameObject);

        // Actually, most things went wrong if I really destroy the player, maybe this is not a good solution
        //Destroy(gameObject);
    }

    public void GetIntoLightSource()
    {
        //Called in OnCollisionEnter2D when the player lights on this lantern and when they enters the light area of this lantern
        isInLightSource = true;
        gainLightTimer = Time.time;
        //TODO - Call function in playerStatus to start adding their light energy, but I don't know how to use coroutine

        lightSystem.IntoLightSourceLightOff();
        //TODO - show some particles or animations
    }

    public void LeaveLightSource()
    {
        //Called by OnCollisionExit2D when the player exits the light area
        isInLightSource = false;
        loseLightTimer = Time.time;
        //TODO - Call function in playerStatus to stop adding their light energy, but I don't know how to use coroutine

        if (state.lightEnergy > 0)
        {
            lightSystem.LightOn();
        }

        //TODO - show some particles or animations
    }

    //Player skill
    public void ObtainLightDraw()
    {
        //state.getLightDraw = true;
        playerAttack.ObtainLightDraw();
    }

    //UI
    private void UpdatePlayerLightEnergy()
    {
        if (isInLightSource)
        {
            if (Time.time - gainLightTimer >= gainLightTime && state.lightEnergy < state.maxLightEnergy)
            {
                state.lightEnergy++;
                gainLightTimer = Time.time;
            }
        }
        else
        {
            if (Time.time - loseLightTimer >= loseLightTime && state.lightEnergy > 0)
            {
                state.lightEnergy--;
                loseLightTimer = Time.time;
            }
            if (state.lightEnergy < lowLight)
            {
                lightSystem.LowLightEnergyWarning();
            }
        }

        if (playerUI != null) playerUI.UpdateLightEnergy(state.lightEnergy);
    }

    public void LoadData(GameData gameData)
    {
        this.state.maxHealth = gameData.maxHealth;
        this.state.maxLightEnergy = gameData.maxLightEnergy;
        //this.state.getLightDraw = gameData.getLightDraw;

        AllRecover();
        //if (this.state.getLightDraw) ObtainLightDraw();
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.maxHealth = this.state.maxHealth;
        gameData.maxLightEnergy = this.state.maxLightEnergy;
        //gameData.getLightDraw = this.state.getLightDraw;
    }
}
