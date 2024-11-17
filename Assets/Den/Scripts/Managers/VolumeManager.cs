using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private VolumeProfile volumeProfile;
    [SerializeField]
    private int lowValue = 5;
    [SerializeField]
    private float maxBlur = 0.3f;
    [SerializeField]
    private float minBlur = 0.02f;

    // You can leave this variable out of your function, so you can reuse it throughout your class.
    private UnityEngine.Rendering.Universal.FilmGrain filmGrain;
    

    private void Start()
    {
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(filmGrain));

        filmGrain.intensity.Override(0.02f);
    }

    private void Update()
    {
        UpdateVisionVolume();
    }

    private void UpdateVisionVolume()
    {
        if (PlayerManager.Instance == null) return;
        else
        {
            if (PlayerManager.Instance.PlayerLightEnergy() <= lowValue - 1)
            {
                filmGrain.intensity.Override((maxBlur * (lowValue - PlayerManager.Instance.PlayerLightEnergy()) / lowValue));
            }
            else
            {
                filmGrain.intensity.Override(minBlur);
            }
        }
    }
}
