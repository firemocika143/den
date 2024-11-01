using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempTreasure : MonoBehaviour
{
    private bool usable = false;
    //private bool open = false;

    private PlayerController pc;
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
        if (usable && pc != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pc.Recover(1000);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(true);
            usable = true;
            Debug.Log("Triggered");
            pc = col.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(false);
            usable = false;
            Debug.Log("Exited");
            pc = null;
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
