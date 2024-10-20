using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PageItem : MonoBehaviour
{
    [Serializable]
    public class Page
    {
        public Sprite graph;
    }

    public Page page; 

    [SerializeField]
    private GameObject sign;
    [SerializeField]
    private Light2D backLight;

    private bool onTrigger;
    private bool beingRead = false;

    private void Start()
    {
        sign.SetActive(false);
    }

    private void Update()
    {
        if (onTrigger && Input.GetKeyDown(KeyCode.E) && !beingRead)
        {
            UIManager.Instance.ReadPage(page);// this is a little bit inefficient
            beingRead = true;
        }
    }

    public void OnTrigger()
    {
        sign.SetActive(true);
        onTrigger = true;
    }

    public void ExitTrigger()
    {
        sign.SetActive(false);
        onTrigger = false;
    }
}
