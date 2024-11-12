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

    [Header("Invincible Time")]
    public float invincibleTime = 1.0f;
    private bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (BEDetector.shoot)
        {
            shooting();
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

            StartCoroutine(InvincibleTimeCount());
        }

    }

    private void shooting()
    {
        //Debug.Log("iscooldown: " + iscooldown);
        //Debug.Log("shoot: " + shoot);
        if (!iscooldown && BEDetector.target != null)
        {
            //Debug.Log("shoot2: " + BEDetector.shoot);
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
                
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().attack = attack;
            bullet.GetComponent<Rigidbody2D>().velocity = (BEDetector.target.position - transform.position) * bulletSpeed;

            StartCoroutine(CooldownCount());
        }
    }

    private IEnumerator InvincibleTimeCount()
    {
        invincible = true;

        yield return new WaitForSeconds(invincibleTime);

        invincible = false;
    }

    private IEnumerator CooldownCount()
    {
        iscooldown = true;

        yield return new WaitForSeconds(cooldown);

        iscooldown = false;
    }
}
