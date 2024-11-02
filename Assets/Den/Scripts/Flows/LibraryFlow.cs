using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryFlow : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    private void Awake()
    {
        GameObject player;
        PlayerController c = FindFirstObjectByType(typeof(PlayerController)) as PlayerController;
        if (c != null)
        {
            player = c.gameObject;
        }
        else
        {
            player = Instantiate(playerPrefab);
        }
        //and then respawn player by player manager
        //also play the music for library
    }
}
