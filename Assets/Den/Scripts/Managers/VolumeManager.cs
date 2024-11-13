using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private VolumeProfile volumeProfile;

    // You can leave this variable out of your function, so you can reuse it throughout your class.
    private UnityEngine.Rendering.Universal.FilmGrain filmGrain;
    private int lowValue = 5;

    private void Start()
    {
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(filmGrain));

        filmGrain.intensity.Override(0.02f);
    }

    private void Update()
    {
        if (PlayerManager.Instance == null) return;
        else
        {
            if (PlayerManager.Instance.PlayerLightEnergy() <= lowValue - 1)
            {
                filmGrain.intensity.Override((0.1f * (lowValue - PlayerManager.Instance.PlayerLightEnergy())));
            }
            else
            {
                filmGrain.intensity.Override(0.02f);
            }
        }
    }


}
