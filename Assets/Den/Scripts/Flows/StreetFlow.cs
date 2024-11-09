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
        public string page2HintText;

        [Header("Page 2 Settings")]
        public Page page1;
        public Vector3 page1Position;
        public string page1HintText;

        [Header("Light Draw Settings")]
        public Skill lightDraw = new Skill("LightDraw", 1);
        public Vector3 lightDrawPosition;
        public string lightDrawHintText;
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
    [SerializeField]
    private GameObject libraryBlocker;

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

        if (Input.GetKeyDown(KeyCode.K))
        {
            // TODO - play a sfx
        }

        if (libraryBlocker.activeSelf && PlayerManager.Instance.PlayerGetLightDraw())
        {
            libraryBlocker.SetActive(false);
        }
    }

    public override void StartFlow()
    {
        GameManager.Instance.ReloadGameProgress();

        UIManager.Instance.FadeMaskOn();// this is like [loading...]
        // TODO - Loading
        // TODO - RespawnPlayer -> that might be right, respawning player in player manager is a little bit of weird, after all, I should set the player respawning point inn here probably
        // TODO - Generate all items
        if (!GameManager.Instance.progress.finishLightOff)
        {
            // close the event triggers and reload to the environment after those events
        }
        if (!GameManager.Instance.progress.getPage2)
        {
            ItemManager.Instance.GeneratePageItem(streetItemSettings.page2, streetItemSettings.page2Position, streetItemSettings.page2HintText, () =>
            {
                GameManager.Instance.progress.getPage2 = true;
            });
        }
        if (!GameManager.Instance.progress.getPage1)
        {
            ItemManager.Instance.GeneratePageItem(streetItemSettings.page1, streetItemSettings.page1Position, streetItemSettings.page1HintText, () =>
            {
                GameManager.Instance.progress.getPage1 = true;
            });
        }
        if (!GameManager.Instance.progress.getLightDraw)
        {
            ItemManager.Instance.GenerateSkillItem(streetItemSettings.lightDraw, streetItemSettings.lightDrawPosition, streetItemSettings.lightDrawHintText, () =>
            {
                GameManager.Instance.progress.getLightDraw = true;
            }); ;
        }

        // TODO - Fade in
        UIManager.Instance.FadeIn();// this is not working on currently
        // SFX & VFX
        SoundManager.Instance.ResetBGM();

    }

    
}
