using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    /// This script would have a lot of functions that can be called by the UI Manager ///
    
    public List<Page> book = new List<Page>();

    public void AddPage(Page newPage)
    {
        if (book.Contains(newPage)) Debug.LogError("Duplicated page");

        book.Add(newPage);
    }
}


