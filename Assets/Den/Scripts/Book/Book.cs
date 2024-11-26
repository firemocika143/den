using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Book
{
    /// This script would have a lot of functions that can be called by the UI Manager ///

    public int id;
    public List<Page> pages;

    public void AddPage(Page newPage)
    {
        if (pages == null)
        {
            pages = new List<Page>();
        }
            
        if (pages.Contains(newPage)) Debug.LogError("Duplicated page");

        pages.Add(newPage);
        UIManager.Instance.LoadPage(newPage);
    }
}


