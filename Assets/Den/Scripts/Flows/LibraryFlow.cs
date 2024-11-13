using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SoundManager;
using static StreetFlow;

public class LibraryFlow : Flow
{
    [Serializable]
    public class LibraryItemSettings
    {
        [Header("Page 2 Settings")]
        public Page page2;
        public Vector3 page2Position;
        public string page2HintText;

        [Header("Light Draw Settings")]
        public Skill lightDraw = new Skill("LightDraw", 1);
        public Vector3 lightDrawPosition;
        public string lightDrawHintText;
    }

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private SoundManager.ClipEnum exploringClip;

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
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerManager.Instance.InstantKillPlayer();
        }
    }

    public override void StartFlow()
    {
        GameManager.Instance.ReloadGameProgress();

        UIManager.Instance.FadeMaskOn();// this is like [loading...]
        // TODO - Loading
        // TODO - RespawnPlayer -> that might be right, respawning player in player manager is a little bit of weird, after all, I should set the player respawning point inn here probably
        // TODO - Generate all items
        //if (!GameManager.Instance.progress.getPage2)
        //{
        //    ItemManager.Instance.GeneratePageItem(streetItemSettings.page2, streetItemSettings.page2Position, streetItemSettings.page2HintText, () =>
        //    {
        //        GameManager.Instance.progress.getPage2 = true;
        //    });
        //}
        //if (!GameManager.Instance.progress.getPage1)
        //{
        //    ItemManager.Instance.GeneratePageItem(streetItemSettings.page1, streetItemSettings.page1Position, streetItemSettings.page1HintText, () =>
        //    {
        //        GameManager.Instance.progress.getPage1 = true;
        //    });
        //}
        //if (!GameManager.Instance.progress.getLightDraw)
        //{
        //    ItemManager.Instance.GenerateSkillItem(streetItemSettings.lightDraw, streetItemSettings.lightDrawPosition, streetItemSettings.lightDrawHintText, () =>
        //    {
        //        GameManager.Instance.progress.getLightDraw = true;
        //    }); ;
        //}
        //if (!GameManager.Instance.progress.getLantern)
        //{
        //    ItemManager.Instance.GenerateSkillItem(streetItemSettings.lantern, streetItemSettings.lanternPosition, streetItemSettings.lanternHintText, () =>
        //    {
        //        GameManager.Instance.progress.getLantern = true;
        //    }); ;
        //}

        // TODO - Fade in
        UIManager.Instance.FadeIn();// this is not working on currently
        // SFX & VFX
        SoundManager.Instance.ResetBGM();
    }
}
