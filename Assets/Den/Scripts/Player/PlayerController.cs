using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static PlayerAnimation;

public class PlayerController : MonoBehaviour, IDataPersistence
{   
    [System.Serializable]
    public class PlayerState
    {
        [Header("Movement State")]
        public bool stop = false;
        public bool climb = false;
        public bool horMoving = false;
        public bool movingUp;
        public bool movingDown;

        //health
        [Header("Health")]
        public int maxHealth;
        public int health;

        //light
        [Header("Light")]
        public int maxLightEnergy;
        public int lightEnergy;

        [Header("Attack")]
        public int attack = 1;
        public bool attacking = false;
        public bool attackEnd = false;

        [Header("Other Player State Settings")]
        public bool resting = true;
        public bool standing = false;
        public bool isHittable = true;
        public bool isDamaged = false;
        public bool dying = false;
        public bool inDanger = false;

        [Header("Skill Obtaining States")]
        public bool getLightDraw = false;
    }

    public PlayerState state;
    public PlayerAnimationState currState;

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
    private PlayerAnimation playerAnimation;

    [Header("The Others")]
    [SerializeField]
    private SpriteRenderer playerSprite;
    [SerializeField]
    private int damageMultiplier;

    private float gainLightTimer; 
    private float loseLightTimer;

    private PlayerMovement playerMovement;
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
        if (TryGetComponent<LineRenderer>(out var lineRenderer))
        {
            lineRenderer.enabled = false;
        }

        InitPlayerState();
        LeaveLightSource();
    }

    private void Update()
    {
        if (lightSystem.enabled)
        {
            // this is double check
            if (state.lightEnergy <= 0 && lightSystem.Lighting())
            {
                //lightSystem.LightOff();
                lightSystem.ChangeLight(PlayerLightSystem.LightState.NOLIGHTENERGY);
            }
            else if (state.lightEnergy > 0 && !lightSystem.Lighting() && !isInLightSource && !state.dying)
            {
                //lightSystem.LightOn();
                lightSystem.ChangeLight(PlayerLightSystem.LightState.WITHLIGHTENERGY);
            }
        }

        UpdatePlayerLightEnergy();

        DecidePlayerAnimation();
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
            //state.isDamaged = true;
            if (state.lightEnergy > 0) state.health -= damage;
            else state.health -= damage * damageMultiplier;

            state.health = state.health > 0 ? state.health : 0;
            UIManager.Instance.UpdatePlayerHealth(state.health);

            if (state.health <= 0)
            {
                PlayerKilled();
            }
            else
            {
                StartCoroutine(PlayerUnhittable(2f));
            }
            //state.isDamaged = false;

            LanternManager.Instance.AllCastFail();
        }
    }

    public void Recover(int recover)
    {
        state.health += recover;
        state.health = state.health <= state.maxHealth ? state.health : state.maxHealth;
        UIManager.Instance.UpdatePlayerHealth(state.health);
    }

    public void AllRecover()
    {
        state.health = state.maxHealth;
        state.lightEnergy = state.maxLightEnergy;

        UIManager.Instance.UpdatePlayerAllState(state.maxHealth, state.health, state.maxLightEnergy, state.lightEnergy);
    }

    public void UseLightEnergy(int val)
    {
        state.lightEnergy = state.lightEnergy - val >= 0 ? state.lightEnergy - val : 0;

        UIManager.Instance.UpdatePlayerLight(state.lightEnergy);
    }

    //Events functions
    public void StopPlayer()
    {
        state.stop = true;
        state.movingUp = false;
        state.movingDown = false;
        state.horMoving = false;
        state.attacking = false;
        state.attackEnd = false;
    }

    private void PlayerKilled()
    {
        // TODO - player dying Animation
        state.dying = true;
        StopPlayer();
        StartCoroutine(playerAnimation.PlayerDieAnimation(() => 
        { 
            PlayerManager.Instance.PlayerRespawn();
            playerAnimation.PlayerRespawnAnimation();
            state.dying = false;
            state.stop = false;
        }));

        // Actually, most things went wrong if I really destroy the player, maybe this is not a good solution
        //Destroy(gameObject);
    }

    public void GetIntoLightSource()
    {
        //Called in OnCollisionEnter2D when the player lights on this lantern and when they enters the light area of this lantern
        isInLightSource = true;
        gainLightTimer = Time.time;

        state.inDanger = false;

        if (lightSystem.enabled) lightSystem.ChangeLight(PlayerLightSystem.LightState.INLIGHTSOURCE);
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
            if (lightSystem.enabled) lightSystem.ChangeLight(PlayerLightSystem.LightState.WITHLIGHTENERGY); ;
        }

        //TODO - show some particles or animations
    }

    private IEnumerator PlayerUnhittable(float time)
    {
        StartCoroutine(PlayerFlash(4, time));

        state.isHittable = false;
        yield return new WaitForSeconds(time);
        state.isHittable = true;
    }

    private void InitPlayerState()
    {
        state.stop = false;
        state.climb = false;
        state.horMoving = false;
        state.movingUp = false;
        state.movingDown = false;

        state.attacking = false;
        state.attackEnd = false;

        state.resting = true;
        state.standing = false;
        state.isHittable = true;
        state.dying = false;
        state.inDanger = false;
    }

    public void InstantKill()
    {
        PlayerManager.Instance.PlayerRespawn();//this is so weird, very weird
        StartCoroutine(PlayerUnhittable(2f));
    }

    //Player skill
    public void ObtainLightDraw()
    {
        playerAttack.ObtainLightDraw();
    }

    //PlayerAnimation
    private void DecidePlayerAnimation()
    {
        if (state.dying)
        {
            return;
        }
        else if (state.attacking)
        {
            if (!state.attackEnd) currState = PlayerAnimationState.ATTACK;
            else currState = PlayerAnimationState.ATTACKEND;
        }
        else
        {
            if (state.resting)
            {
                if (state.standing) currState = PlayerAnimationState.STAND;
                else currState = PlayerAnimationState.REST;
            }
            else if (state.movingDown)
            {
                currState = PlayerAnimationState.FALL;
            }
            else if (state.movingUp)
            {
                currState = PlayerAnimationState.JUMP;
            }
            else if (state.horMoving)
            {
                currState = PlayerAnimationState.WALK;
            }
            else currState = PlayerAnimationState.IDLE;
        }
    }

    private IEnumerator PlayerFlash(int times, float time)
    {
        float pretime = 0.2f;
        yield return new WaitForSeconds(pretime);

        for (int i = 0; i < times; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds((time - pretime) / (float)times / 2);
            playerSprite.enabled = true;
            yield return new WaitForSeconds((time - pretime) / (float)times / 2);
        }
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
                //if (lightSystem.enabled) lightSystem.LowLightEnergyWarning();
                //TODO - SFX

                //this should be in update()
                //and I need a alternative way to show this by playing sfx
                //vfx is too hard to see
            }

            if (state.lightEnergy <= 0 && !state.inDanger)
            {
                if (lightSystem.enabled) lightSystem.ChangeLight(PlayerLightSystem.LightState.NOLIGHTENERGY); ;
                SoundManager.Instance.ChangeClip(SoundManager.Instance.clips.DANGER);
                state.inDanger = true;
            }
        }

        UIManager.Instance.UpdatePlayerLight(state.lightEnergy);
    }

    public void LoadData(GameData gameData)
    {
        //this.state.maxHealth = gameData.maxHealth;
        //this.state.maxLightEnergy = gameData.maxLightEnergy;
        AllRecover();

        if (GameManager.Instance.progress.getLightDraw) ObtainLightDraw();
    }

    public void SaveData(ref GameData gameData)
    {
        //gameData.maxHealth = this.state.maxHealth;
        //gameData.maxLightEnergy = this.state.maxLightEnergy;
    }
}
