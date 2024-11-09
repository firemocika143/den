using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class LibraryFlow : Flow
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private SoundManager.ClipEnum exploringClip;

    public override void StartFlow()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        //// hum... I don't think this a good I idea though
        //if (PlayerManager.Instance != null)
        //{
        //    if (!PlayerManager.Instance.PlayerIsInLightSource() && PlayerManager.Instance.PlayerLightEnergy() > 0)
        //    {
        //        SoundManager.Instance.ChangeClip(exploringClip);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerManager.Instance.InstantKillPlayer();
        }
    }
}
