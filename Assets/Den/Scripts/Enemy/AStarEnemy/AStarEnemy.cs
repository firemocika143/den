using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AStarEnemy : Enemy
{
    [Header("Hang Around Enemy set")]
    public GameObject aStarEnemySet;

    //health
    [Header("Health")]
    public int maxHealth = 10;
    private int health;
   
    private AIPath aipath;

    private Vector2 orig_pos;

    [SerializeField]
    private FlashHandler flashHandler;

    public bool killed = false;

    // Start is called before the first frame update
    void Start()
    {
        orig_pos = transform.position;
        
        Spawn();

        aipath = GetComponent<AIPath>();
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

    public override void Damage(int d)
    {
        if (!invincible)
        {
            health = health - d >= 0 ? health - d : 0;

            if (health <= 0)
            {
                invincible = true;
                killed = true;
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
        killed = false;
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

    //float dir = from.x - player.transform.position.x;

    //if (dir <= 0)
    //{
    //    // TODO - go right up
    //    Vector2 force = new Vector2(hitBackHorizontalForce, hitBackVerticalForce);
    //    StartCoroutine(PerformHitBack(force));
    //}
    //else if (dir > 0)
    //{
    //    // TODO - go left up
    //    Vector2 force = new Vector2(-hitBackHorizontalForce, hitBackVerticalForce);
    //    StartCoroutine(PerformHitBack(force));
    //}

    //private IEnumerator PerformHitBack(Vector2 force)
    //{
    //    player.GetComponent<Rigidbody2D>().velocity = force;
    //    PlayerController pc = player.GetComponent<PlayerController>();
    //    pc.state.hitback = true;
    //    yield return new WaitForSeconds(hitbackInterval);
    //    pc.state.hitback = false;
    //}
}

