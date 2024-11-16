using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkKnight : Enemy
{
    //health
    [Header("Health")]
    public int maxHealth = 500;
    private int health;

    [Header("Attack")]
    public int attack = 1;

    [Header("Invincible Time")]
    public float invincibleTime = 1.0f;
    private bool invincible = false;

    //private float cooldownTime = 1.0f;
    private bool cooldown = false;

    [Header("Normal Attack")]
    public GameObject normalAttack;
    public float normalAttackTime = 0.5f;
    public float normalAttackCooldownTime = 1.0f;
    private bool normalAttackCooldown = false;

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

    [Header("Boss Dark Knight Area")]
    public Transform bossDarkKnightAreaTRansform;

    [Header("Boss Knight Right Arm Movement")]
    public BossKnightRightArmMovement bossKnightRightArmMovement;

    private Rigidbody2D rb;
    private PlayerController pc;
    private Transform targetTRansform;

    [HideInInspector]
    public bool playerIsInBossDarkKnightArea = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        invincible = false;

        rb = GetComponent<Rigidbody2D>();
        pc = FindFirstObjectByType<PlayerController>();
        targetTRansform = PlayerManager.Instance.PlayerTransform();

        normalAttack.SetActive(false);
        skill1FireArea.SetActive(false);
        skill2Knight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cooldown);
        if (!cooldown && playerIsInBossDarkKnightArea)
        {
            if (!skill1Cooldown)
            {
                Skill1();
            }
            else if (!skill2Cooldown && targetTRansform.position.x - bossDarkKnightAreaTRansform.position.x - 9.0f < 0)
            {
                Skill2();
            }
            else if (!normalAttackCooldown)
            {
                StartCoroutine(ShowNormalAttackDetector());
            }
        }
    }

    //Damage Function for Player to Call
    public override void Damage(int d)
    {
        if (!invincible)
        {
            health = health - d >= 0 ? health - d : 0;

            if (health <= 0)
            {
                Destroy(gameObject);
            }

            StartCoroutine(invincibleTimeCount());
        }

    }

    private IEnumerator invincibleTimeCount()
    {
        invincible = true;

        yield return new WaitForSeconds(invincibleTime);

        invincible = false;
    }

    private IEnumerator ShowNormalAttackDetector()
    {
        cooldown = true;
        normalAttack.SetActive(true);
        
        yield return new WaitForSeconds(normalAttackTime);
        
        normalAttack.SetActive(false);

        normalAttackCooldown = true;

        yield return new WaitForSeconds(normalAttackCooldownTime);

        normalAttackCooldown = false;
        cooldown = false; // set cooldown to false, can use other skills
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

        rb.transform.position = new Vector3(bossDarkKnightAreaTRansform.position.x, bossDarkKnightAreaTRansform.transform.position.y, rb.transform.position.z); // set dark knight position
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

    public void BossStart()
    {
        playerIsInBossDarkKnightArea = true;
    }
}
