//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BossDarkKnightNormalAttack : MonoBehaviour
//{
//    [Header("Attack")]
//    public int attack = 1;

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        //Debug.Log(other.gameObject.name);
//        // if I touch player
//        if (other.CompareTag("Player"))
//        {
//            PlayerController playerController = other.GetComponent<PlayerController>();
//            playerController.Damage(attack);
//        }
//    }

//    private void OnTriggerStay2D(Collider2D other)
//    {
//        // if I stay with player
//        if (other.CompareTag("Player"))
//        {
//            PlayerController playerController = other.GetComponent<PlayerController>();
//            playerController.Damage(attack);
//        }
//    }
//}
