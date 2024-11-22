using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-9999)]
public class GameManager : MonoBehaviour
{
    private static bool s_IsShuttingDown = false;

    public static GameManager Instance
    {
        get// set and get just prevent inauthoritized accesses
        {
#if UNITY_EDITOR
            if (s_Instance == null && !s_IsShuttingDown)
            {
                var newInstance = Instantiate(Resources.Load<GameManager>("GameManager"));
                newInstance.Awake();
            }
#endif
            return s_Instance;
        }
        private set => s_Instance = value;
    }

    public static bool IsShuttingDown()
    {
        return s_IsShuttingDown;
    }

    private static GameManager s_Instance;

    private void Awake()
    {
        if (s_Instance == this)
        {
            return;
        }

        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    public class KeySettings
    {
        [Header("Movement Keys")]
        public KeyCode Left = KeyCode.A;
        public KeyCode Right = KeyCode.D;
        public KeyCode Jump = KeyCode.Space;
        public KeyCode Climb = KeyCode.W;

        [Header("Interaction/Skill Keys")]
        public KeyCode Attack = KeyCode.Mouse0;
        public KeyCode Interaction = KeyCode.S;
        public KeyCode LightLantern = KeyCode.S;
    }

    public KeySettings keySettings;

    [Serializable]
    public class GameProgress
    {
        [Header("Street")]
        public bool getPage2 = false;
        public bool getPage4 = false;
        public bool getPage1 = false;
        public bool firstTimeLightOff = true;
        public bool finishLightOff = false;
        
        public bool getLantern = false;

        [Header("Library")]
        public bool getLightDraw = false;
        public bool defeatDarkKnight = false;
    }

    public GameProgress progress;
    public Book book = new Book();
    public int currentPage;

    public Flow flow; // well, this is not that good, I can only use those functions defined in Flow, but not the certain flows
    public string CurrScene = "Street";

    public bool gamePaused = false;// Can utilize this variable to adjust some settings when game is paused

    private float currTimeScale = 1f;

    /// <summary>
    /// This will be called in flows, because DontDestroyOnLoadObjects seems not run Start again in new scenes
    /// </summary>
    public void StartNewScene()
    {
        GetReferences();
    }

    private void GetReferences()
    {
        // TODO - wait for everything to be set up (but how? game manager is always at the first place to run)
        // How to call start flow? should flow be a class or an interface (this should be an easy decision)
        flow = FindFirstObjectByType<Flow>();
        if (flow != null) CurrScene = flow.flowName;
        else Debug.LogError("no flow!");
    }

    public void ManualSave()
    {
        PlayerController pc = FindFirstObjectByType<PlayerController>();
        if (pc != null) pc.AllRecover();

        DataPersistenceManager.instance.SaveGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        // TODO - lower volume of music and SFX
    }

    public void ResumeGame()
    {
        Time.timeScale = currTimeScale;
        gamePaused = false;
        // TODO - restore volume of music and SFX
    }

    public void SlowDown(float val)
    {
        currTimeScale = val;
        Time.timeScale = val;
    }

    public void BackToNormalSpeed()
    {
        currTimeScale = 1f;
        Time.timeScale = 1f;
    }

    //This might be called in flows
    public void ReloadGameProgress()
    {
        // book
        if (UIManager.Instance != null) UIManager.Instance.ReloadBook(book);
        //player respawn point
        //player skill
    }

    //Audio and Screen Settings
}
