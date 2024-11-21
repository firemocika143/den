using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public int attack;

    public int groundLayerNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        // if I touch player
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.Damage(attack, transform.position);

            Destroy(gameObject);
        }

        if (other.gameObject.layer == groundLayerNumber)
        {
            Destroy(gameObject);
        }
    }
}
