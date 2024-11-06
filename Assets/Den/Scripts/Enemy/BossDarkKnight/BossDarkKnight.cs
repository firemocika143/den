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
    private bool invincible;

    [Header("Normal Attack")]
    public GameObject normalAttack;
    public float normalAttackTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        invincible = false;

        normalAttack.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ShowNormalAttackDetector());
    }

    private IEnumerator ShowNormalAttackDetector()
    {
        normalAttack.SetActive(true);
        
        yield return new WaitForSeconds(normalAttackTime);
        
        normalAttack.SetActive(false);
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

}
