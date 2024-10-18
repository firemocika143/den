using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public int RespawnPointNumber;
    private Vector3 respawnPosition;
    private bool onTrigger;

    [SerializeField]
    private GameObject sign;

    private void Start()
    {
        respawnPosition = transform.position;
        sign.SetActive(false);
    }

    private void Update()
    {
        if (onTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SetRespawnPoint();
                DataPersistenceManager.instance.SaveGame();
                //change sign appearance
            }
        }
    }

    public void SetRespawnPoint()
    {
        //GameManager.Instance.

        sign.SetActive(true);
    }

    void EnterTrigger()
    {
        onTrigger = true;

        //camera stop following player
        //camera zoom out, focusing on the respawnPoint 
    }

    void ExitTrigger()
    {
        onTrigger = false;

        //camera zoom in, focusing on the player
        //camera follow player
    }
}
