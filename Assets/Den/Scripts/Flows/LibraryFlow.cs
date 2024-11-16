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
        [Header("Light Draw Settings")]
        public SkillItemInfo lightDrawInfo;

        [Header("Lantern Piece Settings")]
        public List<LanternItemInfo> lanternItems = new List<LanternItemInfo>();
    }

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private SoundManager.ClipEnum exploringClip;
    [SerializeField]
    private LibraryItemSettings libraryItemSettings;

    public void Awake()
    {
        name = "Library";
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
        GenerateAllItems();

        // TODO - Fade in
        UIManager.Instance.FadeIn();// this is not working on currently
        // SFX & VFX
        SoundManager.Instance.ResetBGM();
    }

    private void GenerateAllItems()
    {
        if (!GameManager.Instance.progress.getLightDraw)
        {
            ItemManager.Instance.GenerateSkillItem(libraryItemSettings.lightDrawInfo, () =>
            {
                GameManager.Instance.progress.getLightDraw = true;
            });
        }
        GenerateAllLanternItems();
    }

    private void GenerateAllLanternItems()
    {
        foreach (LanternItemInfo li in libraryItemSettings.lanternItems)
        {
            ItemManager.Instance.GenerateLanternItem(li);
        }
    }
}
