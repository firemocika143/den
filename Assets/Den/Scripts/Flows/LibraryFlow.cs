using Cinemachine;
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

        [Header("Page 5 Settings")]// little map
        public PageItemInfo page5Info;

        [Header("Page 1 Settings")]// little map
        public PageItemInfo page1Info;

        [Header("Page 2 Settings")]// little map
        public PageItemInfo page2Info;

        [Header("Page 3 Settings")]// little map
        public PageItemInfo page3Info;

        [Header("Lantern Piece Settings")]
        public List<LanternItemInfo> lanternItems = new List<LanternItemInfo>();
    }

    [Serializable]
    public class LibraryShortCuts
    {
        [Header("Light Draw")]
        public Transform shortCut_LD;

        [Header("Boss Room")]
        public Transform shortCut_Boss;
    }

    [SerializeField]
    private SoundManager.ClipEnum exploringClip;
    [SerializeField]
    public LibraryItemSettings libraryItemSettings;
    [SerializeField]
    private CinemachineVirtualCamera main_v_cam;
    [SerializeField]
    private LibraryShortCuts lsc;

    private float killTimer;
    private float killTime = 7f;

    

    public void Awake()
    {
        flowName = "Library";
        GameManager.Instance.StartNewScene();
    }

    public void Start()
    {
        StartFlow();// this is actually a little bit of funny
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && Time.time - killTimer > killTime)
        {
            //this should be a normal kill but with dark fade in and out, and a number fade in at the same time displaying how many times the player had killed themselves
            PlayerManager.Instance.KillPlayer();
            GameManager.Instance.killTimes++;
            UIManager.Instance.UpdatePlayerKillTime();
            killTimer = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerManager.Instance.TPPlayerTo(lsc.shortCut_Boss.position);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerManager.Instance.TPPlayerTo(lsc.shortCut_LD.position);
        }
    }

    public override void StartFlow()
    {
        GameManager.Instance.ReloadGameProgress();

        UIManager.Instance.FadeMaskOn();// this is like [loading...]
        // TODO - Loading
        // TODO - RespawnPlayer -> that might be right, respawning player in player manager is a little bit of weird, after all, I should set the player respawning point inn here probably
        PlayerManager.Instance.ResetPlayerLanternPiece();
        // TODO - Generate all items
        GenerateAllItems();

        // TODO - Fade in
        UIManager.Instance.FadeIn();// this is not working on currently
        // SFX & VFX
        SoundManager.Instance.ResetBGM();
    }

    public override void ReloadFlow()
    {
        EnemyManager.Instance.RespawnAllEnemy();
        PlayerManager.Instance.PlayerRespawn();
        PlayerManager.Instance.ResetPlayerLanternPiece();
        GenerateAllLanternItems();
        CameraManager.Instance.SwitchVirtualCamera(main_v_cam);
        VolumeManager.Instance.ResetVolume();
        //something work weird here
        DataPersistenceManager.instance.LoadGame();
        // TODO - UI fade in
        UIManager.Instance.FadeIn(1.5f);// this should wait
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
        if (!GameManager.Instance.progress.getPage5)
        {
            ItemManager.Instance.GeneratePageItem(libraryItemSettings.page5Info, () =>
            {
                GameManager.Instance.progress.getPage5 = true;
            });
        }
        if (!GameManager.Instance.progress.getPage3)
        {
            ItemManager.Instance.GeneratePageItem(libraryItemSettings.page3Info, () =>
            {
                GameManager.Instance.progress.getPage3 = true;
            });
        }
        if (!GameManager.Instance.progress.getPage2)
        {
            ItemManager.Instance.GeneratePageItem(libraryItemSettings.page2Info, () =>
            {
                GameManager.Instance.progress.getPage2 = true;
            });
        }
        if (!GameManager.Instance.progress.getPage1)
        {
            ItemManager.Instance.GeneratePageItem(libraryItemSettings.page1Info, () =>
            {
                GameManager.Instance.progress.getPage1 = true;
            });
        }
        GenerateAllLanternItems();
    }

    private void GenerateAllLanternItems()
    {
        ItemManager.Instance.ClearLanternItems();

        foreach (LanternItemInfo li in libraryItemSettings.lanternItems)
        {
            ItemManager.Instance.GenerateLanternItem(li);
        }
    }
}
