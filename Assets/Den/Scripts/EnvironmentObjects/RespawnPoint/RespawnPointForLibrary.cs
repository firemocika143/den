using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointForLibrary : MonoBehaviour
{
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
                GameManager.Instance.ManualSave();

                //change sign appearance
                if (!sign.activeSelf) StartCoroutine(ShowSaveSign());
            }
        }
    }

    public void EnterTrigger()
    {
        onTrigger = true;

        //camera stop following player
        //camera zoom out, focusing on the respawnPoint 
    }

    public void ExitTrigger()
    {
        onTrigger = false;

        //camera zoom in, focusing on the player
        //camera follow player
    }

    private IEnumerator ShowSaveSign()
    {
        sign.SetActive(true);
        yield return new WaitForSeconds(3);
        sign.SetActive(false);
    }
}
