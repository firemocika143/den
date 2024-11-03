using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLightOff : MonoBehaviour
{
    [SerializeField]
    private StreetFlow flow;//I am not sure if this is good

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();
            pc.StopPlayer();
            StartCoroutine(flow.StreetLightOff());
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
