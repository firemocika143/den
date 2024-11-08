using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Page", menuName = "Page")]
public class Page : ScriptableObject
{
    public int pageID;

    public GameObject pagePanelPrefab;// if I can make it a sprite, just like drawing it in somewhere out of unity, I guess that's better
}
