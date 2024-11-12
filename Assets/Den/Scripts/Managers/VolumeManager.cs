using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private VolumeProfile volumeProfile;

    private void Update()
    {
        if (PlayerManager.Instance == null) return;
        else
        {
            if (PlayerManager.Instance.PlayerLightEnergy() <= 4)
            {
                if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

                // You can leave this variable out of your function, so you can reuse it throughout your class.
                UnityEngine.Rendering.Universal.FilmGrain filmGrain;

                if (!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(filmGrain));

                filmGrain.intensity.Override((0.1f * (5 - PlayerManager.Instance.PlayerLightEnergy())));
            }
            else
            {
                if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

                // You can leave this variable out of your function, so you can reuse it throughout your class.
                UnityEngine.Rendering.Universal.FilmGrain filmGrain;

                if (!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(filmGrain));

                filmGrain.intensity.Override(0.02f);
            }
        }
    }


}
