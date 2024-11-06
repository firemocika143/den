using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryFlow : Flow
{
    [SerializeField]
    private GameObject playerPrefab;

    public override void StartFlow()
    {
        throw new System.NotImplementedException();
    }

    //private void Awake()
    //{
    //    GameObject player;
    //    PlayerController c = FindFirstObjectByType(typeof(PlayerController)) as PlayerController;
    //    if (c != null)
    //    {
    //        player = c.gameObject;
    //    }
    //    else
    //    {
    //        player = Instantiate(playerPrefab);
    //    }
    //    //and then respawn player by player manager
    //    //also play the music for library
    //}
}
