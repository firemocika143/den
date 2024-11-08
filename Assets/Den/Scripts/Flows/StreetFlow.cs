using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetFlow : Flow
{
    [Serializable]
    public class StreetItemSettings
    {
        [Header("Page 2 Settings")]
        public Page page2;
        public Vector3 page2Position;

        [Header("Light Draw Settings")]
        public Vector3 lightDrawPosition;
    }

    // should I make an static instance for this script?
    [SerializeField]
    private GameObject firstLamp;
    [SerializeField]
    private Transform centerPointOfLamps;
    [SerializeField]
    private float zoomOutRadius;
    [SerializeField]
    private GameObject firstLightSource;

    private bool first;
    private Coroutine eventCoroutine;
    //private bool inStreetLightOffEvent;

    public void Awake()
    {
        name = "Street";
    }

    private void Update()
    {
        if (PlayerManager.Instance.PlayerLightEnergy() <= 0)
        {
            PlayerManager.Instance.InstantKillPlayer();
        }
    }

    public override void StartFlow()
    {
        if (first)
        {
            // TODO - Loading
            // TODO - RespawnPlayer
            // TODO - Generate all items
            if (!GameManager.Instance.progress.finishLightOff)
            {

                return;
            }
            // TODO - Fade in
            UIManager.Instance.FadeIn();// this is not working on currently
            // SFX & VFX
            SoundManager.Instance.ResetBGM();

            first = false;
        }
    }

    
}
