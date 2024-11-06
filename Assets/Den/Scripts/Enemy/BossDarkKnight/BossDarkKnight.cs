using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkKnight : MonoBehaviour, IEnemy
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

    [Header("Normal Attack")]
    public GameObject normalAttack;
    public float normalAttackTime = 0.5f;
    public float normalAttackCooldownTime = 1.0f;
    private bool normalAttackCooldown = false;

    [Header("Skill 1")]
    public float jumpPower = 8.0f;
    public float skill1CooldownTime = 5.0f;
    private bool skill1Cooldown = false;

    private Rigidbody2D rb;
    private PlayerController pc;
    private Transform target = PlayerManager.Instance.PlayerTransform();


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        invincible = false;

        rb = GetComponent<Rigidbody2D>();
        pc = FindFirstObjectByType<PlayerController>();
        target = PlayerManager.Instance.PlayerTransform();

        normalAttack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        target = PlayerManager.Instance.PlayerTransform();
        if (!skill1Cooldown)
        {
            Jump();
        }
        
        if (!normalAttackCooldown)
        {
            StartCoroutine(ShowNormalAttackDetector());
        }
        
    }

    private IEnumerator ShowNormalAttackDetector()
    {
        normalAttack.SetActive(true);
        
        yield return new WaitForSeconds(normalAttackTime);
        
        normalAttack.SetActive(false);

        normalAttackCooldown = true;

        yield return new WaitForSeconds(normalAttackCooldownTime);

        normalAttackCooldown = false;
    }

    //Damage Function for Player to Call
    public void Damage(int d)
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

    private void Jump()
    {
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        //if (target.position.x - rb.transform.position.x > 0.5f)
        //{
        //    rb.AddForce(Vector2.left * jumpPower, ForceMode2D.Impulse);
        //}
        //else if (target.position.x - rb.transform.position.x < 0.5f)
        //{
        //    rb.AddForce(Vector2.right * jumpPower * 10, ForceMode2D.Impulse);
        //}

        
        StartCoroutine(skill1CooldownTimeCount());
    }

    private IEnumerator skill1CooldownTimeCount()
    {
        skill1Cooldown = true;

        yield return new WaitForSeconds(skill1CooldownTime);

        skill1Cooldown = false;
    }
}
