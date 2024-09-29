using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangAroundEnemyStatus : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
