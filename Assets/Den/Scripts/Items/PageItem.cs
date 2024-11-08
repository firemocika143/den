using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PageItem : MonoBehaviour, IItem
{
    public Page page;
    public string hintText;
    public Action gameRecord;

    public void Get()
    {
        GameManager.Instance.book.AddPage(page);
        UIManager.Instance.ShowHint(hintText);
        gameRecord?.Invoke();
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
