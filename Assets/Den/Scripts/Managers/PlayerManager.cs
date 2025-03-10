using Cinemachine;
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
    public int playerLanternPiece = 0;

    private float killTimer;
    private float killTime = 7f;

    private void Awake()
    {
        Instance = this;

        PlayerRespawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && Time.time - killTimer > killTime)
        {
            if (GameManager.Instance.flow.flowName == "Street")
            {

            }
            else if (GameManager.Instance.flow.flowName == "Library")
            {
                //this should be a normal kill but with dark fade in and out, and a number fade in at the same time displaying how many times the player had killed themselves
                PlayerManager.Instance.KillPlayer();
                GameManager.Instance.killTimes++;
                UIManager.Instance.UpdatePlayerKillTime();
                killTimer = Time.time;
            }
        }
    }

    public void PlayerRespawn()//passing in a game object is usually bad, this is a temp solution
    {
        if (player == null)
        {
            // Maybe I need to find if destroying the player is the correct decision, and also the way to controll the progress and the camera
            PlayerController pc = FindFirstObjectByType<PlayerController>();// does anyone really do this?
            if (pc != null)
            {
                player = pc.gameObject;
            }
            else
            {
                player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
            }
        }

        player.transform.position = spawnPoint.position;
                
        StartCoroutine(WaitToStart(1f));
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

    public Transform PlayerTransform()
    {
        return player.transform;
    }

    public void EnablePlayerToMove()
    {
        player.GetComponent<PlayerController>().state.stop = false;
    }

    public void StopPlayer()
    {
        player.GetComponent<PlayerController>().StopPlayer();
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

    public void KillPlayer()
    {
        player.GetComponent<PlayerController>().KillPlayer();
    }

    public bool PlayerIsInLightSource()
    {
        return player.GetComponent<PlayerController>().state.isInLightSource;
    }

    public bool PlayerGetLightDraw()
    {
        return !(player.GetComponent<PlayerSkill>().lightDraw == null);
    }

    private void SetPlayerMaxLightEnergy(int val)
    {
        //this will be called in flows, actually a temporary solution
        if (player == null)
        {
            Debug.LogError("Try to change player energy while player is not on the field");
            return;
        }

        player.GetComponent<PlayerController>().state.maxLightEnergy = val;
        player.GetComponent<PlayerController>().state.lightEnergy = val;
    }

    public void AddPlayerMaxLightEnergy(int val)
    {
        if (player == null)
        {
            Debug.LogError("Try to change player energy while player is not on the field");
            return;
        }

        PlayerController pc = player.GetComponent<PlayerController>();
        pc.state.maxLightEnergy += val;
        pc.state.lightEnergy += val;
    }

    public void GivePlayerForce(Vector2 force)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity += force;
        }
    }

    public void PlayerStopClimbing()
    {
        player.GetComponent<PlayerController>().state.climb = false;
    }

    public bool PlayerOnObstacle()
    {
        return player.GetComponent<PlayerController>().state.onObstacle;
    }

    public void ResetPlayerLanternPiece()
    {
        playerLanternPiece = GameManager.Instance.playerFirmPiece;
        SetPlayerMaxLightEnergy(120 + GameManager.Instance.playerFirmPiece * 30);
        UIManager.Instance.UpdatePlayerPiece(playerLanternPiece);
        player.GetComponent<PlayerLightSystem>().ResetMaxRadius();
        player.GetComponent<PlayerLightSystem>().AddMaxRadius(0.1f * (float)GameManager.Instance.playerFirmPiece);
    }

    public void AddPlayerLanternPiece()
    {
        playerLanternPiece++;
        AddPlayerMaxLightEnergy(30);
        UIManager.Instance.UpdatePlayerPiece(playerLanternPiece);
        player.GetComponent<PlayerLightSystem>().AddMaxRadius(0.1f);
    }

    public void PlayerReload()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        pc.ReloadAfterKilled();
    }

    public bool PlayerIsInDanger()
    {
        return player.GetComponent<PlayerController>().state.inDanger;
    }

    public void PlayerAllRecover()
    {
        player.GetComponent<PlayerController>().AllRecover();
    }

    public void TPPlayerTo(Vector2 pos)
    {
        player.transform.position = pos;
    }
}
