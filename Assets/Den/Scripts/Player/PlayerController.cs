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
        public bool isInLightSource = false;
        public bool hitback = false;
        public bool onObstacle = false;

        [Header("Skill Obtaining States")]
        public bool getLightDraw = false;
    }

    public PlayerState state;
    public PlayerAnimationState currState;

    [Header("Hitback Settings")]
    [SerializeField]
    private float hitBackHorizontalForce = 4f;
    [SerializeField]
    private float hitBackVerticalForce = 8f;
    [SerializeField]
    private float hitbackInterval = 0.5f;

    //light
    [Header("Other Light Settings")]
    public int lowLight = 20;
    [SerializeField]
    private float gainLightTime = 0.2f;
    [SerializeField]
    private float normalLoseLightTime = 1f;

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
    private PlayerSFX playerSFX;
    private PlayerParticle playerParticle;
    private float unhittableTimer;
    private float loseLightTime = 1f;
    private float loseLightTimeMultiplier = 1f;

    private Device activatingDevice = null;
    [SerializeField]
    private AudioSource returnPlayer;

    private void Start()
    {
        state.isInLightSource = false;
        gainLightTimer = Time.time;
        loseLightTimer = Time.time;
        //loseLightCoroutine = StartCoroutine(LoseLight());

        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        playerSFX = GetComponent<PlayerSFX>();
        playerParticle = GetComponent<PlayerParticle>();
        if (TryGetComponent<LineRenderer>(out var lineRenderer))
        {
            lineRenderer.enabled = false;
        }

        InitPlayerState();
        LeaveLightSource();

        loseLightTime = normalLoseLightTime;

        VolumeManager.Instance.SetPlayerLowLightValue(lowLight);
    }

    private void Update()
    {
        UpdatePlayerLightEnergy();
        DecidePlayerAnimation();
        if (activatingDevice != null)
        {
            CheckActivatingDeviceState();
        }

        // Update player light
        lightSystem.UpdatePlayerLight(state.lightEnergy, state.maxLightEnergy);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("LightSource"))
        {
            GetIntoLightSource();
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("LightSource"))
        {
            state.isInLightSource = true;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            HitOnObstacle();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            LeaveObstacle();
        }
    }

    //Player State Functions
    public void Damage(int damage)
    {
        if (state.isHittable)
        {
            UIManager.Instance.GetDamage();
            //state.isDamaged = true;
            if (state.lightEnergy > 0) state.health -= damage;
            else state.health -= damage * damageMultiplier;

            if (activatingDevice != null)
            {
                activatingDevice.InterruptCharging();
            }

            state.health = state.health > 0 ? state.health : 0;
            UIManager.Instance.UpdatePlayerHealth(state.health);

            if (state.health <= 0)
            {
                PlayerKilled();
            }
            else
            {
                StartCoroutine(PlayerUnhittable(1.5f));
            }
        }
    }

    public void Damage(int damage, Vector2 enemyPosition)
    {
        if (state.isHittable)
        {
            UIManager.Instance.GetDamage();
            //state.isDamaged = true;
            if (state.lightEnergy > 0) state.health -= damage;
            else state.health -= damage * damageMultiplier;

            if (activatingDevice != null)
            {
                activatingDevice.InterruptCharging();
            }

            if (state.health > 0)
            {
                HitBackPlayer(enemyPosition);
            }

            state.health = state.health > 0 ? state.health : 0;
            UIManager.Instance.UpdatePlayerHealth(state.health);

            if (state.health <= 0)
            {
                PlayerKilled();
            }
            else
            {
                StartCoroutine(PlayerUnhittable(1.5f));
            }
            //state.isDamaged = false;

            //LanternManager.Instance.AllCastFail();
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

        UIManager.Instance.UpdatePlayerHealth(state.health);
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
        state.isHittable = false;
        StopPlayer();
        SoundManager.Instance.ChangeClip(SoundManager.ClipEnum.NULL);

        if (activatingDevice != null)
        {
            activatingDevice.InterruptCharging();
            LeaveDevice();
        }

        //SoundManager.Instance.ResetBGM();
        returnPlayer.Play();
        
        StartCoroutine(playerAnimation.PlayerDieAnimation(() => 
        {
            UIManager.Instance.FadeOut(() => 
            {
                // well, this is readible and free to customize in every scene, but quite inefficient and is actually weird for calling variable in this script out in the other script
                if (GameManager.Instance.flow == null)
                {
                    Debug.LogError("no flow in this scene");
                }
                else GameManager.Instance.flow.ReloadFlow();
                ReloadAfterKilled();
                // play sfx
            });
        }));

        // Actually, most things went wrong if I really destroy the player, maybe this is not a good solution
        //Destroy(gameObject);
    }

    public void KillPlayer()
    {
        PlayerKilled();
    }

    public void GetIntoLightSource()
    {
        //Called in OnCollisionEnter2D when the player lights on this lantern and when they enters the light area of this lantern
        state.isInLightSource = true;
        gainLightTimer = Time.time;

        state.inDanger = false;
        //TODO - show some particles or animations
    }

    public void LeaveLightSource()
    {
        //Called by OnCollisionExit2D when the player exits the light area
        state.isInLightSource = false;
        loseLightTimer = Time.time;
        //TODO - Call function in playerStatus to stop adding their light energy, but I don't know how to use coroutine

        //TODO - show some particles or animations
    }

    private IEnumerator PlayerUnhittable(float time)
    {
        StartCoroutine(PlayerFlash(3, time));

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
        StartCoroutine(PlayerUnhittable(1.5f));
    }

    private void HitBackPlayer(Vector2 from)
    {
        float dir = from.x - transform.position.x;

        if (dir <= 0)
        {
            // TODO - go right up
            Vector2 force = new Vector2(hitBackHorizontalForce, hitBackVerticalForce);
            StartCoroutine(PerformHitBack(force));
        }
        else if (dir > 0)
        {
            // TODO - go left up
            Vector2 force = new Vector2(-hitBackHorizontalForce, hitBackVerticalForce);
            StartCoroutine(PerformHitBack(force));
        }
    }

    private IEnumerator PerformHitBack(Vector2 force)
    {
        state.hitback = true;
        yield return new WaitForSeconds(0.1f);

        GetComponent<Rigidbody2D>().velocity = force;
        yield return new WaitForSeconds(hitbackInterval);
        state.hitback = false;
    }

    private void HitOnObstacle()
    {
        // higher light intensity
        lightSystem.SetIntensity(1.3f);
        // TODO - enhance volume
        VolumeManager.Instance.QuickFilmIn();
        // TODO - slowdown player
        GameManager.Instance.SlowDown(0.6f);
        state.onObstacle = true;
        loseLightTimeMultiplier = 0.5f;
    }

    private void LeaveObstacle()
    {
        lightSystem.SetBackIntensity();
        // return volume
        VolumeManager.Instance.QuickFilmOut();
        GameManager.Instance.BackToNormalSpeed();
        state.onObstacle = true;
        loseLightTimeMultiplier = 1f;
    }

    public void MatchDevice(Device device)
    {
        activatingDevice = device;
    }

    public void LeaveDevice()
    {
        activatingDevice = null;
    }

    private void CheckActivatingDeviceState()
    {
        if (activatingDevice == null)
        {
            Debug.LogError("How?");
            return;
        }

        if (activatingDevice.charged) activatingDevice = null;
    }

    public void ChangePlayerUseLightRate(float newInterval = 0.1f)
    {
        // be used for player attack and devices and obstacle
        loseLightTime = newInterval;
    }

    public void ChangeBackPlayerUseLightRate()
    {
        // be used for player attack and devices and obstacle
        loseLightTime = normalLoseLightTime;
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
        else if (state.attacking || state.attackEnd)
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
        playerSprite.enabled = false;
        yield return new WaitForSeconds(0.125f);
        playerSprite.enabled = true;
        yield return new WaitForSeconds(0.125f);

        for (int i = 0; i < times; i++)
        {
            playerSprite.enabled = false;
            yield return new WaitForSeconds((time - 0.125f) / (float)times / 2);
            playerSprite.enabled = true;
            yield return new WaitForSeconds((time - 0.125f) / (float)times / 2);
        }
    }

    //UI
    private void UpdatePlayerLightEnergy()
    {
        if (state.isInLightSource)
        {
            if (Time.time - gainLightTimer >= gainLightTime && state.lightEnergy < state.maxLightEnergy)
            {
                state.lightEnergy++;
                gainLightTimer = Time.time;
                playerSFX.PlayGetEnergySFX();
                playerParticle.PlayRecoverParticle();
            }
            else if (state.lightEnergy >= state.maxLightEnergy)
            {
                playerSFX.StopGetSFX();
                playerParticle.StopRecoverParticle();
            }
        }
        else
        {
            playerSFX.StopGetSFX();
            playerParticle.StopRecoverParticle();

            if (Time.time - loseLightTimer >= loseLightTime * loseLightTimeMultiplier && state.lightEnergy > 0)
            {
                state.lightEnergy--;
                loseLightTimer = Time.time;
            }

            if (state.lightEnergy <= 0 && !state.inDanger && GameManager.Instance.CurrScene != "Street")// 
            {
                SoundManager.Instance.ChangeClip(SoundManager.Instance.clips.DANGER);
                state.inDanger = true;
            }
        }
    }

    public void ReloadAfterKilled()
    {
        playerAnimation.PlayerRespawnAnimation();
        state.dying = false;
        state.isHittable = true;
    }

    public void LoadData(GameData gameData)
    {
        if (GameManager.Instance.progress.getLightDraw) ObtainLightDraw();

        AllRecover();
    }

    public void SaveData(ref GameData gameData)
    {
        
    }
}
