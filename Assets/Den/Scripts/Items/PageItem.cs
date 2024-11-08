using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PageItem : MonoBehaviour, IItem
{
    public Page page;

    public void Get()
    {
        GameManager.Instance.book.AddPage(page);
        // TODO - some animation that shows the player has get the page
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Get();
        }
    }
}

//public Page page;

//[SerializeField]
//private GameObject sign;

//private bool onTrigger;
//private bool beingRead = false;

//private void Start()
//{
//    sign.SetActive(false);
//}

//private void Update()
//{
//    if (onTrigger && Input.GetKeyDown(KeyCode.E) && !UIManager.Instance.PauseMenuOpened())// I don't think check if game paused here is good though 
//    {
//        if (!beingRead)
//        {
//            UIManager.Instance.ReadPage(page);// this is a little bit inefficient
//            beingRead = true;
//        }
//        else
//        {
//            UIManager.Instance.ClosePage();// this is a little bit inefficient
//            beingRead = false;
//        }
//    }
//}

//public void OnTrigger()
//{
//    sign.SetActive(true);
//    onTrigger = true;
//}

//public void ExitTrigger()
//{
//    sign.SetActive(false);
//    onTrigger = false;
//}
