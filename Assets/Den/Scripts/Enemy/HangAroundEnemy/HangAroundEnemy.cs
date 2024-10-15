using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangAroundEnemy : MonoBehaviour, IEnemy
{

    //health
    [Header("Health")]
    public int maxHealth = 10;
    private int health;

    [Header("Attack")]
    public int attack = 1;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Damage Function for Player to Call
    public void Damage(int d)
    {
        health = health - d >= 0 ? health - d : 0;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
