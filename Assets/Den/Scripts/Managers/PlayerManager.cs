using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

[DefaultExecutionOrder(10001)]
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private PlayerUI playerUI;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        Instance = this;

        player = FindFirstObjectByType<PlayerController>().gameObject;// does anyone really do this?
        PlayerRespawn();
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

    public void UpdateHealth()
    {
        if (player.TryGetComponent<PlayerController>(out var pc))
        {
            playerUI.UpdateHealth(pc.state.health);
        }
    }

    public void UpdateMaxHealth()
    {
        if (player.TryGetComponent<PlayerController>(out var pc))
        {
            playerUI.UpdateMaxHealth(pc.state.maxHealth);
        }
    }
    public void UpdateLight()
    {
        if (player.TryGetComponent<PlayerController>(out var pc))
        {
            playerUI.UpdateLightEnergy(pc.state.lightEnergy);
        }
    }

    public void UpdateMaxLight()
    {
        if (player.TryGetComponent<PlayerController>(out var pc))
        {
            playerUI.UpdateMaxLightEnergy(pc.state.maxLightEnergy);
        }
    }

    public void UpdateAll()
    {
        if (player.TryGetComponent<PlayerController>(out var pc))
        {
            playerUI.UpdateAll(pc.state.maxHealth, pc.state.health, pc.state.maxLightEnergy, pc.state.lightEnergy);
        }
    }

    private IEnumerator WaitToStart(float time)
    {
        yield return null;

        if (player.TryGetComponent<PlayerController>(out var pc))
        {
            pc.state.resting = true;
            pc.StopPlayer();
            yield return new WaitForSeconds(time);
            pc.state.stop = false;
        }
    }
}
