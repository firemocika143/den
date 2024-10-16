using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour, IEnemy
{
    //health
    [Header("Health")]
    public int maxHealth = 10;
    private int health;

    [Header("Attack")]
    public int attack = 1;

    [Header("Bullet Speed")]
    public float bulletSpeed = 1.0f;

    [Header("Bullet")]
    public GameObject bullet;
    public float cooldown = 80.0f;
    private bool iscooldown;

    [Header("Player Detector")]
    public GameObject playerDetector;
    private bool shoot;

    [Header("Invincible Time")]
    public float invincibleTime = 1.0f;
    private bool invincible;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        invincible = false;
        iscooldown = false;
        shoot = playerDetector.GetComponent<BulletEnemyPlayerDetector>().shoot;
    }

    void Update()
    {
        shooting();
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

    private void shooting()
    {
        Debug.Log("iscooldown: " + iscooldown);
        //Debug.Log("shoot: " + shoot);
        if (!iscooldown)
        {
            //Debug.Log("iscooldown2: " + iscooldown);
            shoot = playerDetector.GetComponent<BulletEnemyPlayerDetector>().shoot;

            if (shoot)
            {
                Debug.Log("shoot2: " + shoot);
                GameObject newBullet = GameObject.Instantiate(bullet);
                newBullet.transform.position = transform.position;
                newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0f);

                StartCoroutine(cooldownCount());

                shoot = false;
            }
        }
    }

    private IEnumerator cooldownCount()
    {
        iscooldown = true;

        yield return new WaitForSeconds(cooldown);

        iscooldown = false;
    }
}
