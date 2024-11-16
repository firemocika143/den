using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroEnemy : Enemy
{
    [Header("Patro Enemy Set")]
    public GameObject patroEnemySet;

    //health
    [Header("Health")]
    public int maxHealth = 10;
    private int health;

    [Header("Attack")]
    public int attack = 1;

    [Header("Invincible Time")]
    public float invincibleTime = 1.0f;
    private bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        invincible = false;
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

    //Damage Function for Player to Call
    public override void Damage(int d)
    {
        if (!invincible)
        {
            health = health - d >= 0 ? health - d : 0;

            if (health <= 0)
            {
                //Destroy(gameObject);
                patroEnemySet.SetActive(false);
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
}

