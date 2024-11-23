using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PageItem : MonoBehaviour, IItem
{
    public PageItemInfo info;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Get();
        }
    }

    public void Get()
    {
        GameManager.Instance.book.AddPage(info.page);
        UIManager.Instance.ShowHint(info.hintText);
        info.gameRecord?.Invoke();
        Debug.Log("Get page");
        // TODO - some animation that shows the player has get the page
        Destroy(gameObject);
    }
}
