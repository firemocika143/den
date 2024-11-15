using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StreetFlow : Flow, IDataPersistence
{
    [Serializable]
    public class StreetItemSettings
    {
        [Header("Page 1 Settings")]
        public Page page2;
        public Transform page2Pos;
        public string page2HintText;

        [Header("Page 2 Settings")]
        public Page page1;
        public Transform page1Pos;
        public string page1HintText;

        [Header("Light Draw Settings")]
        public Skill lightDraw = new Skill("LightDraw", 1);
        public Transform lightDrawPos;
        public string lightDrawHintText;

        [Header("Lantern Settings")]
        public Skill lantern = new Skill("Lantern", 0);
        public Transform lanternPos;
        public string lanternHintText;

        [Header("LanternSettings")]
        public List<LanternItem> lanternItems = new List<LanternItem>();

        [Header("DeviceSettings")]
        public List<Device> device;
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
        // TODO - RespawnPlayer -> that might be right, respawning player in player manager is a little bit of weird, after all, I should set the player respawning point inn here probably
        // TODO - Generate all items
        //if (!GameManager.Instance.progress.finishLightOff)
        //{
        //    // close the event triggers and reload to the environment after those events
        //}
        if (!GameManager.Instance.progress.getPage2)
        {
            ItemManager.Instance.GeneratePageItem(streetItemSettings.page2, streetItemSettings.page2Pos.position, streetItemSettings.page2HintText, () =>
            {
                GameManager.Instance.progress.getPage2 = true;
            });
        }
        if (!GameManager.Instance.progress.getPage1)
        {
            ItemManager.Instance.GeneratePageItem(streetItemSettings.page1, streetItemSettings.page1Pos.position, streetItemSettings.page1HintText, () =>
            {
                GameManager.Instance.progress.getPage1 = true;
            });
        }
        if (!GameManager.Instance.progress.getLightDraw)
        {
            ItemManager.Instance.GenerateSkillItem(streetItemSettings.lightDraw, streetItemSettings.lightDrawPos.position, streetItemSettings.lightDrawHintText, () =>
            {
                GameManager.Instance.progress.getLightDraw = true;
            }); ;
        }
        if (!GameManager.Instance.progress.getLantern)
        {
            ItemManager.Instance.GenerateSkillItem(streetItemSettings.lantern, streetItemSettings.lanternPos.position, streetItemSettings.lanternHintText, () =>
            {
                GameManager.Instance.progress.getLantern = true;
            }); ;
        }

        // TODO - Fade in
        UIManager.Instance.FadeIn();// this is not working on currently
        // SFX & VFX
        SoundManager.Instance.ResetBGM();
    }

    public void LoadData(GameData gameData)
    {

    }

    public void SaveData(ref GameData gameData)
    {

    }

    private void OnDestroy()
    {
                
    }
}
