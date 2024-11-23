using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private Vector2 orig_pos;

    [SerializeField]
    private FlashHandler flashHandler;

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

    public override void Damage(int d)
    {
        if (!invincible)
        {
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

    protected override void HitFlash(Action after = null)
    {
        flashHandler.Flash(after);
    }
}
