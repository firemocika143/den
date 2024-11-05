using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

[DefaultExecutionOrder(10001)]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    
    public Transform spawnPoint;

    [SerializeField]
    private GameObject playerPrefab;

    private GameObject player;

    private void Start()
    {
        Instance = this;

        player = FindFirstObjectByType<PlayerController>().gameObject;// does anyone really do this?
        if (player != null) PlayerRespawn();
    }

    public void PlayerRespawn()//passing in a game object is usually bad, this is a temp solution
    {
        // Maybe I need to find if destroying the player is the correct decision, and also the way to controll the progress and the camera
        // GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        if (player == null)
        {
            player = Instantiate(playerPrefab, spawnPoint);
        }

        player.transform.position = spawnPoint.position;
        CameraManager.Instance.Follow(player.transform);
        StartCoroutine(WaitToStart(1f));
        //something work weird here
        DataPersistenceManager.instance.LoadGame();
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
}
