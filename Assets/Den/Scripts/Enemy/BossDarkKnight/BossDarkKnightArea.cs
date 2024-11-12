using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkKnightArea : MonoBehaviour
{
    public BossDarkKnight bossDarkKnight;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.name);
        // if I touch player
        if (other.CompareTag("Player"))
        {
            bossDarkKnight.BossStart();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bossDarkKnight.BossStart();
        }
    }
}
