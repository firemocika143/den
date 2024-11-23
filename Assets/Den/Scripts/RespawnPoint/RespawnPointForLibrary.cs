using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointForLibrary : MonoBehaviour
{
    private Vector3 respawnPosition;

    private void Start()
    {
        respawnPosition = transform.position;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GameManager.Instance.ManualSave();
            PlayerManager.Instance.PlayerAllRecover();
        }
    }
}
