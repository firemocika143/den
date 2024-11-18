using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangAroundEnemy : Enemy
{
    [Header("Hang Around Enemy set")]
    public GameObject hangAroundEnemySet;

    //health
    [Header("Health")]
    public int maxHealth = 10;
    private int health;

    [Header("Attack")]
    public int attack = 1;

    public float invincibleTime = 1.0f;
    private bool invincible;
    private Vector2 orig_pos;

    // Start is called before the first frame update
    void Start()
    {
        orig_pos = transform.position;

        Spawn();
    }

    void OnEnable()
    {
        health = maxHealth;
        invincible = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        // if I touch player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Damage(attack);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if I stay with player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Damage(attack);
        }
    }

    private IEnumerator InvincibleTimeCount()
    {
        invincible = true;

        yield return new WaitForSeconds(invincibleTime);

        invincible = false;
    }

    public override void Damage(int d)
    {
        if (!invincible)
        {
            health = health - d >= 0 ? health - d : 0;

            if (health <= 0)
            {
                Kill();
            }
            else
            {
                StartCoroutine(InvincibleTimeCount());
            }
        }

    }

    public override void Spawn()
    {
        health = maxHealth;
        invincible = false;
        transform.position = orig_pos;
    }

    public override void Kill()
    {
        StopAllCoroutines();
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
