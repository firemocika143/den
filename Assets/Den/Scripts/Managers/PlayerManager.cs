using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

[DefaultExecutionOrder(-9000)]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    
    public Transform spawnPoint;

    [SerializeField]
    private GameObject playerPrefab;

    private GameObject player;

    private void Awake()
    {
        PlayerRespawn();

        Instance = this;
    }

    public void PlayerRespawn()//passing in a game object is usually bad, this is a temp solution
    {
        // Maybe I need to find if destroying the player is the correct decision, and also the way to controll the progress and the camera
        PlayerController pc = FindFirstObjectByType<PlayerController>();// does anyone really do this?
        if (pc != null)
        {
            player = pc.gameObject;
        }

        if (player == null)
        {
            player = Instantiate(playerPrefab);
        }

        player.transform.position = spawnPoint.position;
        StartCoroutine(WaitToStart(1f));
        //something work weird here
        if (DataPersistenceManager.instance != null) DataPersistenceManager.instance.LoadGame();
    }

    private IEnumerator WaitToStart(float time)
    {
        yield return null;

        if (player.TryGetComponent<PlayerController>(out var pc))
        {
            //pc.state.resting = true;
            pc.StopPlayer();
            yield return new WaitForSeconds(time);
            pc.state.stop = false;
        }
    }

    public void PlayerInDanger()
    {
        // TODO - switch music
        if (SoundManager.Instance != null) SoundManager.Instance.PlayInDanger();
        // Enhance enemy effects
        // start to cost health or higher enemy attack or enable the player to kill themselves
    }

    public void PlayerInLightSource()
    {
        // TODO - switch music
        if (SoundManager.Instance != null) SoundManager.Instance.PlayInLightSource();
        // Enhance enemy effects
        // start to cost health or higher enemy attack or enable the player to kill themselves
    }

    public Transform PlayerTransform()
    {
        return player.transform;
    }

    public void EnablePlayerToMove()
    {
        player.GetComponent<PlayerController>().state.stop = false;
    }

    public void DisableLightOn()
    {
        PlayerLightSystem pls = player.GetComponent<PlayerLightSystem>();
        pls.LightOff();
        pls.enabled = false;
    }

    public void EnableLightOn()
    {
        PlayerLightSystem pls = player.GetComponent<PlayerLightSystem>();
        
        pls.enabled = true;
    }

    public BoxCollider2D PlayerCollider()
    {
        // this is just for oneWayPlatform
        if (player == null) return null;

        return player.GetComponent<BoxCollider2D>();
    }

    public int PlayerLightEnergy()
    {
        return player.GetComponent<PlayerController>().state.lightEnergy;
    }

    public void InstantKillPlayer()
    {
        player.GetComponent<PlayerController>().InstantKill();
    }
}
