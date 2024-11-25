using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Enemy
{
    //health
    [Header("Health")]
    public int maxHealth = 10;
    private int health;

    [Header("Attack")]
    public int attack = 1;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 1.0f;
    public float cooldown = 2f;
    private bool iscooldown = false;

    [Header("Detector")]
    public BulletEnemyDetector BEDetector;

    private Vector2 orig_pos;

    [SerializeField]
    private FlashHandler flashHandler;

    private bool killed = false;

    // Start is called before the first frame update
    void Start()
    {
        orig_pos = transform.position;

        Spawn();
    }

    void Update()
    {
        if (BEDetector.shoot)
        {
            shooting();
        }
    }

    private void shooting()
    {
        if (!iscooldown && BEDetector.target != null && !killed)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
                
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().attack = attack;
            bullet.GetComponent<Rigidbody2D>().velocity = (BEDetector.target.position - transform.position) * bulletSpeed;

            StartCoroutine(CooldownCount());
        }
    }

    private IEnumerator CooldownCount()
    {
        iscooldown = true;

        yield return new WaitForSeconds(cooldown);

        iscooldown = false;
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
        transform.position = orig_pos;
        iscooldown = false;
        invincible = false;
        killed = false;
    }

    public override void Kill()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    protected override void HitFlash(Action after = null)
    {
        flashHandler.Flash(after);
    }
}
