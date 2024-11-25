using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(10000)]
public class StreetFlow : Flow //, IDataPersistence
{
    [Serializable]
    public class StreetItemSettings
    {
        //[Header("Page 1 Settings")]
        //public PageItemInfo page1Info;

        [Header("Page 4 Settings")]
        public PageItemInfo page4Info;

        [Header("Lantern Piece Settings")]
        public List<LanternItemInfo> lanternItems = new List<LanternItemInfo>();
    }

    // should I make an static instance for this script?
    [SerializeField]
    private StreetItemSettings streetItemSettings;

    [SerializeField]
    private AudioSource killSFXPlayer;

    public bool lightOffEnd = false;
    public bool isInLightOffEvent = false;

    private float killTimer = 0;
    private float killTime = 2f;

    public void Awake()
    {
        flowName = "Street";// this actually change its name in the scene
        GameManager.Instance.StartNewScene();
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

        if (Input.GetKeyDown(KeyCode.K) && Time.time - killTimer > killTime)
        {
            // TODO - play a sfx of cancel
            killTimer = Time.time;
            killSFXPlayer.Play();
            PlayerManager.Instance.InstantKillPlayer();
        }
    }

    public override void StartFlow()
    {
        GameManager.Instance.ReloadGameProgress();

        UIManager.Instance.FadeMaskOn();// this is like [loading...]
        // TODO - Loading
        // TODO - ResetPlayerState -> that might be right, respawning player in player manager is a little bit of weird, after all, I should set the player respawning point inn here probably
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
        //throw new NotImplementedException();
    }

    private void GenerateAllItems()
    {
        if (!GameManager.Instance.progress.getPage4)
        {
            ItemManager.Instance.GeneratePageItem(streetItemSettings.page4Info, () =>
            {
                GameManager.Instance.progress.getPage4 = true;
            });
        }
        //if (!GameManager.Instance.progress.getPage1)
        //{
        //    ItemManager.Instance.GeneratePageItem(streetItemSettings.page1Info, () =>
        //    {
        //        GameManager.Instance.progress.getPage1 = true;
        //    });
        //}
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
