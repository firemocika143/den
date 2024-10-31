using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperLink : MonoBehaviour
{
    [SerializeField]
    private string url;

    public void OpenLink()
    {
        Application.OpenURL(url);
    }
}
