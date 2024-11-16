using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarEnemy : Enemy
{
    [Header("Hang Around Enemy set")]
    public GameObject aStarEnemySet;

    //health
    [Header("Health")]
    public int maxHealth = 10;
    private int health;

    public float invincibleTime = 1.0f;
    private bool invincible;

    private AIPath aipath;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        invincible = false;

        aipath = GetComponent<AIPath>();
    }

    void OnEnable()
    {
        health = maxHealth;
        invincible = false;

        //aipath = GetComponent<AIPath>();
    }

    void Update()
    {
        if (aipath.desiredVelocity.x >= 0.01f) // change direction
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aipath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f); ;
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
                aStarEnemySet.SetActive(false);
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

