using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public float skill2KnightTime = 0.5f;
    public float skill2stunningTime = 0.5f;
    public float skill2CooldownTime = 20.0f;
    private bool skill2Cooldown = false;

    [Header("Skill3")]
    [SerializeField]
    private LightOn lantern1;
    [SerializeField]
    private LightOn lantern2;
    [SerializeField]
    private float skill3BlowingTime = 1.0f;
    [SerializeField]
    private float skill3CooldownTime = 20.0f;
    private bool skill3Cooldown = false;

    [Header("Boss Dark Knight Area")]
    public Transform bossDarkKnightAreaTransform;

    [Header("Boss Knight Right Arm Movement")]
    public BossKnightRightArmMovement bossKnightRightArmMovement;

    [Header("Trigger Trap Settings")]
    [SerializeField]
    private int trapDamage = 5;

    private Rigidbody2D rb;
    private Transform targetTRansform;
    private Vector2 orig_pos;
    private BossDarkKnightMovement movement;

    [HideInInspector]
    public bool playerIsInBossDarkKnightArea = false;

    [SerializeField]
    private FlashHandler flashHandler;

    // Start is called before the first frame update
    void Start()
    {
        orig_pos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<BossDarkKnightMovement>();
        movement.enabled = false;
        //pc = FindFirstObjectByType<PlayerController>();
        targetTRansform = PlayerManager.Instance.PlayerTransform();

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(cooldown);
        if (!cooldown && playerIsInBossDarkKnightArea)
        {
            if (!skill3Cooldown && (lantern1.LightIsOn() || lantern2.LightIsOn()))
            {
                Skill3();
            }
            else if (!skill1Cooldown)
            {
                Skill1();
            }
            else if (!skill2Cooldown && targetTRansform.position.x - bossDarkKnightAreaTransform.position.x - 9.0f < 0)
            {
                Skill2();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if I touch player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Damage(hitDamage);
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
        //bool right = false;
        //if (targetTRansform.position.x - bossDarkKnightAreaTRansform.position.x < 0)
        //{
        //    skill2Knight.transform.position = new Vector3(targetTRansform.position.x + 9.0f, rb.transform.position.y - 2.0f, rb.transform.position.z); // set knight position
        //}
        //else
        //{
        //    skill2Knight.transform.position = new Vector3(targetTRansform.position.x - 9.0f, rb.transform.position.y - 2.0f, rb.transform.position.z); // set knight position
        //    var localScale = skill2Knight.transform.localScale;
        //    localScale.x *= -1;
        //    skill2Knight.transform.localScale = localScale;
        //    right = true;
        //}
        skill2Knight.transform.position = new Vector3(targetTRansform.position.x + 9.0f, rb.transform.position.y + 1.0f, rb.transform.position.z); // set knight position
        yield return new WaitForSeconds(skill2KnightTime);
        skill2Knight.SetActive(false);
        //skill2Knight.transform.position = new Vector3(23.15f, 2.327281f, rb.transform.position.z); // set knight
        //if (right)
        //{
        //    var localScale = skill2Knight.transform.localScale;
        //    localScale.x *= -1;
        //    skill2Knight.transform.localScale = localScale;
        //}

        // fall
        //rb.constraints = RigidbodyConstraints2D.None;
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

    private void Skill3()
    {
        StartCoroutine(TurnOffLights());
    }

    private IEnumerator TurnOffLights()
    {
        cooldown = true;
        skill3Cooldown = true;

        //bossDarkKnightSkill3trigger.SetActive(true);

        yield return new WaitForSeconds(skill3BlowingTime);

        if (lantern1.LightIsOn())
        {
            lantern1.TurnOff();
        }

        if (lantern2.LightIsOn())
        {
            lantern2.TurnOff();
        }

        //bossDarkKnightSkill3trigger.SetActive(false);

        cooldown = false; // set cooldown to false, can use other skills

        // skill 3 cooldown
        yield return new WaitForSeconds(skill3CooldownTime);
        skill3Cooldown = false;
    }

    public void TrapTriggered()
    {
        Damage(trapDamage);
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
            Destroy(this.gameObject);
            return;
        }

        StopAllCoroutines();

        health = maxHealth;
        invincible = false;
        transform.position = orig_pos;
        playerIsInBossDarkKnightArea = false;
        movement.enabled = false;

        skill1FireArea.SetActive(false);
        skill2Knight.SetActive(false);
    }

    public override void Kill()
    {
        StopAllCoroutines();
        GameManager.Instance.progress.defeatDarkKnight = true;
        gameObject.SetActive(false);
    }

    protected override void HitFlash(Action after = null)
    {
        flashHandler.Flash(after);
    }
}
