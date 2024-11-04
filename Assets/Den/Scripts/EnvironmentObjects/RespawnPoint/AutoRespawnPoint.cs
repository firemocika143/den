using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRespawnPoint : MonoBehaviour
{
    public Transform point;
    public bool used = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !used)
        {
            PlayerManager.Instance.spawnPoint = point;
            used = true;
            //should I manual save here?
        }
    }
}
