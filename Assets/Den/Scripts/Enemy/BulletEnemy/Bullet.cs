using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletEnemy;

    private int attack;

    // Start is called before the first frame update
    void Start()
    {
        attack = bulletEnemy.GetComponent< BulletEnemy>().attack;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        // if I touch player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Damage(attack);

            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if I stay with player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Damage(attack);

            Destroy(gameObject);
        }
    }
}
