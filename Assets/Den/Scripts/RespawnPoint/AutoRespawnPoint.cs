using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRespawnPoint : MonoBehaviour
{
    public Transform point;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerManager.Instance.spawnPoint = point;
            //should I manual save here?
        }
    }
}
