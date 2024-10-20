using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageUI : MonoBehaviour
{
    public Image pageImage;

    public void LoadPage(PageItem.Page p)
    {
        pageImage.sprite = p.graph;
        // TODO - set page panel active, which is like a manager's work
    }
}
