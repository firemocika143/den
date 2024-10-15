using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroEnemy : MonoBehaviour, IEnemy
{
    //health
    [Header("Health")]
    public int maxHealth = 10;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        this.health = this.maxHealth;
    }

    public void Damage(int d)
    {
        health = health - d >= 0 ? health - d : 0;
        Debug.Log("damaging enemy");

        if (health <= 0)
        {
            Destroy(gameObject);//well, maybe we wouldn't do this later
        }
    }
}
