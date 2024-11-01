using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
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

    //[Serializable]
    //public class SkillItems
    //{
    //    [Header("Skill Items")]
    //    public GameObject lightDrawItem;
    //}

    //public SkillItems skillItems;

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
        public KeyCode Interaction = KeyCode.E;
        public KeyCode FacePlayerTurnOnLight = KeyCode.S;
    }

    public KeySettings keySettings;

    public string currScene = "Street";

    public bool gamePaused = false;// Can utilize this variable to adjust some settings when game is paused

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
        Time.timeScale = 1f;
        gamePaused = false;
        // TODO - restore volume of music and SFX
    }

    public void SlowDown()
    {
        Time.timeScale = 0.3f;
    }

    public void BackToNormalSpeed()
    {
        Time.timeScale = 1f;
    }

    //Player Dead CutScene
    //Player Respawn
    /// <summary>
    /// Called in PlayerController if a player is dying. Instantiating a new player object with some same max values, also put the player to the spawn point
    /// actually this should be run at the start of every game and every respawn
    /// </summary>
    //Audio and Screen Settings
    //Game Progress

    //Fix Camera
    //Follow player
    //Switch Camera
    //zoom in and out (special skill)
}
