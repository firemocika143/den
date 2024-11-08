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
        public Skill lightDraw = new Skill("LightDraw", 1);
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
    [SerializeField]
    private StreetItemSettings streetItemSettings;

    private Coroutine eventCoroutine;

    public void Awake()
    {
        name = "Street";
    }

    public void Start()
    {
        StartFlow();// this is actually a little bit of funny
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
        UIManager.Instance.FadeMaskOn();// this is like [loading...]
        // TODO - Loading
        // TODO - RespawnPlayer -> that might be right, respawning player in player manager is a little bit of weird, after all, I should set the player respawning point inn here probably
        // TODO - Generate all items
        if (!GameManager.Instance.progress.finishLightOff)
        {

        }
        if (!GameManager.Instance.progress.getPage2)
        {
            ItemManager.Instance.GeneratePageItem(streetItemSettings.page2, streetItemSettings.page2Position);
        }
        if (!GameManager.Instance.progress.getLightDraw)
        {
            ItemManager.Instance.GenerateSkillItem(streetItemSettings.lightDraw, streetItemSettings.lightDrawPosition);
        }

        // TODO - Fade in
        UIManager.Instance.FadeIn();// this is not working on currently
        // SFX & VFX
        SoundManager.Instance.ResetBGM();

    }

    
}
