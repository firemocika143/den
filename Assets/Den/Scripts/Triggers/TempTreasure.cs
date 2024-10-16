using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempTreasure : MonoBehaviour
{
    private bool usable = false;
    //private bool open = false;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject sign;

    private void Start()
    {
        sign.SetActive(false);
    }

    private void Update()
    {
        //if (usable && !open)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        playerController.Recover(1000);
        //        open = true;
        //    }
        //}
        //else if (usable && open)
        //{
        //    if (Input.GetKeyDown(KeyCode.E))
        //    {
        //        Debug.Log("Second press, and nothing happens");
        //        open = false;
        //    }
        //}
        if (usable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerController.Recover(1000);
            }
        }
    }

    // Start is called before the first frame update
    public void Close()
    {
        sign.SetActive(true);
        usable = true;
        Debug.Log("Triggered");
    }

    public void Leave()
    {
        sign.SetActive(false);
        usable = false;
        Debug.Log("Exited");
    }

}
