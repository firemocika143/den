using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StreetFlow : Flow //, IDataPersistence
{
    [Serializable]
    public class StreetItemSettings
    {
        [Header("Page 1 Settings")]
        public PageItemInfo page1Info;

        [Header("Page 2 Settings")]
        public PageItemInfo page2Info;

        [Header("Light Draw Settings")]
        public SkillItemInfo lightDrawInfo;

        [Header("Lantern Piece Settings")]
        public List<LanternItemInfo> lanternItems = new List<LanternItemInfo>();
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

    public bool lightOffEnd = false;

    public bool isInLightOffEvent = false;

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
            //if (isInLightOffEvent)
            //{
            //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //}
            //PlayerManager.Instance.InstantKillPlayer();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            // TODO - play a sfx
            PlayerManager.Instance.InstantKillPlayer();
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
        // TODO - ResetPlayerState -> that might be right, respawning player in player manager is a little bit of weird, after all, I should set the player respawning point inn here probably
        PlayerManager.Instance.SetPlayerMaxLightEnergy(5);
        // TODO - Generate all items
        GenerateAllItems();

        // TODO - Fade in
        UIManager.Instance.FadeIn();// this is not working on currently
        // SFX & VFX
        SoundManager.Instance.ResetBGM();
    }

    private void GenerateAllItems()
    {
        if (!GameManager.Instance.progress.getPage2)
        {
            ItemManager.Instance.GeneratePageItem(streetItemSettings.page2Info, () =>
            {
                GameManager.Instance.progress.getPage2 = true;
            });
        }
        if (!GameManager.Instance.progress.getPage1)
        {
            ItemManager.Instance.GeneratePageItem(streetItemSettings.page1Info, () =>
            {
                GameManager.Instance.progress.getPage1 = true;
            });
        }
        if (!GameManager.Instance.progress.getLightDraw)
        {
            ItemManager.Instance.GenerateSkillItem(streetItemSettings.lightDrawInfo, () =>
            {
                GameManager.Instance.progress.getLightDraw = true;
            });
        }
        GenerateAllLanternItems();
    }

    private void GenerateAllLanternItems()
    {
        foreach(LanternItemInfo li in streetItemSettings.lanternItems)
        {
            ItemManager.Instance.GenerateLanternItem(li);
        }
    }

    //public void LoadData(GameData gameData)
    //{

    //}

    //public void SaveData(ref GameData gameData)
    //{

    //}

    //private void OnDestroy()
    //{
                
    //}
}
