using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private PlayerUI playerUI;

    public void ManualSave()
    {
        

    }






    //Player Dead Animation

    //Player Respawn
    /// <summary>
    /// Called in PlayerController? Instantiating a new player object with some same max values, also put the player to the last saved position
    /// </summary>
    public void PlayerRespawn()
    {
        GameObject player = Instantiate(playerPrefab);
        //set player transform.position
    }

    public void RecoverPlayer()
    {
        PlayerController pc = FindFirstObjectByType<PlayerController>();
        if (pc != null)
        {
            pc.state.health = pc.state.maxHealth;
        }
    }
    //Audio and Screen Settings
    //Game Progress

    //Fix Camera
    //Follow player
    //Switch Camera
    //zoom in and out (special skill)
}
