using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class BossDarkKnight : Enemy
{
    //[Header("Boss Dark Knight Set")]
    //public GameObject bossDarkKnightSet;

    //health
    [Header("Health")]
    public int maxHealth = 500;
    private int health;

    [Header("Attack")]
    public int attack = 1;

    private bool cooldown = false;

    [Header("Normal Attack")]
    private int hitDamage = 1;

    [Header("Skill 1")]
    public GameObject skill1FireArea;
    public float skill1FireAreaTime = 3.0f;
    public float jumpPower = 15.0f;
    public float skill1CooldownTime = 20.0f;
    private bool skill1Cooldown = false;

    [Header("Skill 2")]
    public GameObject skill2Knight;
    public float skill2KnightTime = 0.7f;
    public float skill2stunningTime = 0.5f;
    public float skill2CooldownTime = 20.0f;
    private bool skill2Cooldown = false;

    [Header("Turn Off Light Triggered")]
    [SerializeField]
    private LightOnDevice lantern1;
    [SerializeField]
    private LightOnDevice lantern2;
    [SerializeField]
    private float turnOffLightBlowingTime = 1.0f;
    [SerializeField]
    private int turnOffLightDamage = 1;
    [SerializeField]
    private float knockbackDurationTime = 0.6f;

    private bool touchLightSource = false;

    [Header("Boss Dark Knight Area")]
    public Transform bossDarkKnightAreaTransform;

    [Header("Boss Knight Right Arm Movement")]
    public BossKnightRightArmMovement bossKnightRightArmMovement;

    private Rigidbody2D rb;
    private Transform targetTRansform;
    private Vector2 orig_pos;
    private BossDarkKnightMovement movement;
    private bool turningOffLights = false;
    

    [HideInInspector]
    public bool playerIsInBossDarkKnightArea = false;

    [SerializeField]
    private FlashHandler flashHandler;
    [SerializeField]
    private Gate gate;
    [SerializeField]
    private Transform mid_pos;

    // animation states
    public bool usingAttack1 = false;
    public bool readyToAttack = false;


    // Start is called before the first frame update
    void Start()
    {
        orig_pos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<BossDarkKnightMovement>();
        movement.enabled = false;
        targetTRansform = PlayerManager.Instance.PlayerTransform();

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (touchLightSource)
        {
            //TurnOffLightsTriggered();
            TurnOffLightsTriggered();
        }
        // Debug.Log(cooldown);
        if (!cooldown && playerIsInBossDarkKnightArea)
        {
            if (!skill1Cooldown)//else 
            {
                StartCoroutine(UseSkillWait(()=> { Skill1(); }));
            }
            else if (!skill2Cooldown && targetTRansform.position.x - bossDarkKnightAreaTransform.position.x - 9.0f < 0)
            {
                StartCoroutine(UseSkillWait(() => { Skill2(); }));
            }
        }
    }

    //I think maybe we don't need this
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if I touch player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Damage(hitDamage);
        }

        if (other.CompareTag("LightSource"))
        {
            touchLightSource = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if I stay with player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Damage(hitDamage);
        }

        if (other.CompareTag("LightSource"))
        {
            touchLightSource = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("LightSource"))
        {
            touchLightSource = false;
        }
    }

    private IEnumerator UseSkillWait(Action after = null, float t = 1f)
    {
        if (cooldown)
        {
            Debug.LogError("Try run another skill when 1 is still using");
            yield break;
        }
        cooldown = true;
        readyToAttack = true;
        yield return new WaitForSeconds(t);
        readyToAttack = false;
        after?.Invoke();
    }

    private void Skill1()
    {
        StartCoroutine(EnhanceGravity());
    }

    private IEnumerator EnhanceGravity()
    {
        cooldown = true;
        skill1Cooldown = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); // jump
        yield return new WaitForSeconds(0.1f); // Small delay to allow the jump

        usingAttack1 = true;
        while (rb.velocity.y > 0) // Wait until reaching the jump peak
        {
            yield return null;
        }

        // Temporarily increase gravity scale to land faster
        rb.gravityScale = 20f;

        // Wait until object lands
        while (rb.velocity.y < 0)
        {
            yield return null;
        }

        rb.constraints = RigidbodyConstraints2D.None;
        usingAttack1 = false;
        // Reset gravity scale
        rb.gravityScale = 1f; // Reset to default gravity scale

        // set fire area
        skill1FireArea.transform.position = new Vector3(rb.transform.position.x, skill1FireArea.transform.position.y, skill1FireArea.transform.position.z);
        skill1FireArea.SetActive(true);
        yield return new WaitForSeconds(skill1FireAreaTime);
        skill1FireArea.SetActive(false);

        cooldown = false; // set cooldown to false, can use other skills

        // skill 1 cooldown
        yield return new WaitForSeconds(skill1CooldownTime);
        skill1Cooldown = false;
    }

    private void Skill2()
    {
        targetTRansform = PlayerManager.Instance.PlayerTransform();
        StartCoroutine(SummonKnight());
    }

    private IEnumerator SummonKnight()
    {
        cooldown = true;
        skill2Cooldown = true;

        rb.transform.position = new Vector3(bossDarkKnightAreaTransform.position.x, bossDarkKnightAreaTransform.transform.position.y, rb.transform.position.z); // set dark knight position
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;// freeze dark knight
        yield return new WaitForSeconds(skill2stunningTime); // stunning before summon knight

        skill2Knight.SetActive(true);
        bossKnightRightArmMovement.startMoving();

        skill2Knight.transform.position = new Vector3(targetTRansform.position.x + 9.0f, rb.transform.position.y + 1.0f, rb.transform.position.z); // set knight position
        yield return new WaitForSeconds(skill2KnightTime);
        skill2Knight.SetActive(false);

        // fall
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        while (rb.velocity.y < 0)
        {
            yield return null;
        }

        rb.constraints = RigidbodyConstraints2D.None;
        yield return new WaitForSeconds(skill2stunningTime);
        cooldown = false; // set cooldown to false, can use other skills

        // skill 2 cooldown
        yield return new WaitForSeconds(skill2CooldownTime);
        skill2Cooldown = false;
    }

    private void TurnOffLightsTriggered()
    {
        touchLightSource = false;
        StartCoroutine(TurnOffLights());
    }

    private IEnumerator TurnOffLights()
    {
        if (turningOffLights) yield break;
        Damage(turnOffLightDamage);

        turningOffLights = true;
        Vector2 knockBackDirection = (new Vector2((transform.position.x - mid_pos.position.x) * -1, transform.position.y - mid_pos.position.y)).normalized;
        //Debug.Log(knockBackDirection.x);
        movement.Knockback(knockBackDirection);
        movement.enabled = false;
        yield return new WaitForSeconds(knockbackDurationTime);

        //yield return new WaitForSeconds(turnOffLightBlowingTime); // for animation
        //Debug.Log(knockBackDirection.x);
        if (knockBackDirection.x > 0 && lantern1.charged)
        {
            lantern1.ShutDownDevice();
            Debug.Log("turn off light");
        }
        else if (knockBackDirection.x < 0 && lantern2.charged)
        {
            lantern2.ShutDownDevice();
            Debug.Log("turn off light");
        }
        movement.enabled = true;
        turningOffLights = false;
    }

    public void BossStart()
    {
        playerIsInBossDarkKnightArea = true;
        movement.enabled = true;
    }

    public override void Damage(int d)
    {
        if (!invincible)
        {
            Debug.Log(health);
            health = health - d >= 0 ? health - d : 0;

            if (health <= 0)
            {
                invincible = true;
                HitFlash(() => { Kill(); });
            }
            else
            {
                HitFlash();
                StartCoroutine(InvincibleTimeCount());
            }
        }

    }

    public override void Spawn()
    {
        if (GameManager.Instance.progress.defeatDarkKnight)
        {
            skill1FireArea.SetActive(false);
            skill2Knight.SetActive(false);
            Destroy(this.transform.parent.gameObject);// destroy the whole set
            return;
        }

        StopAllCoroutines();

        health = maxHealth;
        invincible = false;
        transform.position = orig_pos;
        playerIsInBossDarkKnightArea = false;
        movement.enabled = false;
        usingAttack1 = false;
        readyToAttack = false;
        turningOffLights = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = Vector3.zero;
        skill2Cooldown = false;
        cooldown = false; // set cooldown to false, can use other skills
        skill1Cooldown = false;
        turningOffLights = false;

        skill1FireArea.SetActive(false);
        skill2Knight.SetActive(false);

        //Debug.Log("Spawn Boss");
        //Debug.Log(movement.enabled);
        //Debug.Log(transform.position);
        //Debug.Log(orig_pos);
    }

    public override void Kill()
    {
        StopAllCoroutines();
        GameManager.Instance.progress.defeatDarkKnight = true;
        SoundManager.Instance.ChangeClip(SoundManager.ClipEnum.NULL);
        gate.OpenGate();
        CameraManager.Instance.SwitchBackToMainCamera();
        Destroy(bossDarkKnightAreaTransform.gameObject);
        Destroy(skill1FireArea);// destroy the whole set
        Destroy(skill2Knight);// destroy the whole set
        gameObject.SetActive(false);
    }

    protected override void HitFlash(Action after = null)
    {
        flashHandler.Flash(after);
    }
}
