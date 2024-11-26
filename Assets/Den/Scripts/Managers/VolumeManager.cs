using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System;

[DefaultExecutionOrder(-9100)]
public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set; }

    [SerializeField]
    private VolumeProfile volumeProfile;
    [SerializeField]
    private float maxBlur = 0.2f;
    [SerializeField]
    private float minBlur = 0.02f;

    // You can leave this variable out of your function, so you can reuse it throughout your class.
    private UnityEngine.Rendering.Universal.FilmGrain filmGrain;
    private int lowValue = 20;
    private float percentage = 0;
    //private float targetPercentage = 1;

    //private Coroutine co_checker;

    private void Awake()
    {
        Instance = this;

        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(filmGrain));
    }

    private void Start()
    {
        ResetVolume();
    }

    private void Update()
    {
        UpdateVisionVolume();
    }

    private void UpdateVisionVolume()
    {
        if (PlayerManager.Instance == null) return;
        else if (!PlayerManager.Instance.PlayerOnObstacle())
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

    public void SetPlayerLowLightValue(int low_val)
    {
        lowValue = low_val;
    }

    public void QuickFilmIn()
    {
        //StopAllCoroutines();

        //StartCoroutine(Fade(0.2f, 1f));
        filmGrain.intensity.Override(maxBlur);
    }

    public void QuickFilmOut()
    {
        //StopAllCoroutines();

        //StartCoroutine(Fade(0.2f, 0f));
        if (PlayerManager.Instance.PlayerLightEnergy() <= lowValue - 1) return;

        filmGrain.intensity.Override(minBlur);
    }

    public void ResetVolume()
    {
        filmGrain.intensity.Override(minBlur);
    }

    private IEnumerator Fade(float switchTime, float targetPercentage = 0, Action after_fade_out = null)
    {
        if (targetPercentage == percentage)
        {
            yield return null;
        }
        else if (targetPercentage > percentage)
        {
            while (percentage < targetPercentage)
            {
                filmGrain.intensity.Override(Mathf.Lerp(minBlur, maxBlur, percentage));
                percentage += Time.deltaTime / switchTime;
                yield return null;
            }
        }
        else
        {
            while (percentage > targetPercentage)
            {
                filmGrain.intensity.Override(Mathf.Lerp(minBlur, maxBlur * 3 / 2, percentage));
                percentage -= Time.deltaTime / switchTime;
                yield return null;
            }
        }

        percentage = targetPercentage;

        after_fade_out?.Invoke();
    }

    //public void UpdateVolume(float newVolume)//, float targetMinPercent
    //{
    //    if (newVolume < minBlur || newVolume > maxBlur)
    //    {
    //        Debug.LogWarning("New Volume Out Of Range");
    //        return;
    //    }

    //    targetPercentage = (newVolume - minBlur) / (maxBlur - minBlur);
    //}

    //public void StartUpdateVolume()
    //{
    //    if (co_checker != null)
    //    {
    //        Debug.LogError("Trying to start more than 1 coroutines at a time");
    //        return;
    //    }

    //    co_checker = StartCoroutine(UpdatePlayerLightRadius());
    //}

    //private IEnumerator UpdatePlayerLightRadius()
    //{
    //    while (true)
    //    {
    //        if (targetPercentage == percentage)
    //        {
    //            yield return null;
    //        }
    //        else if (targetPercentage > percentage)
    //        {
    //            while (percentage < targetPercentage)
    //            {
    //                filmGrain.intensity.Override(Mathf.Lerp(minBlur, maxBlur * 3 / 2, percentage));
    //                percentage += (float)addAmount / (float)currentPlayerMaxE;

    //                float interval = targetPercentage - percentage >= 0.1 ? fastUpdateInterval : normalUpdateInterval;
    //                yield return new WaitForSeconds(interval);

    //                currentLightRadius = playerLight.pointLightOuterRadius;
    //            }
    //        }
    //        else
    //        {
    //            while (percentage > targetPercentage)
    //            {
    //                playerLight.pointLightOuterRadius = Mathf.Lerp(minRadius, maxRadius, percentage);
    //                percentage -= (float)addAmount / (float)currentPlayerMaxE;

    //                float interval = percentage - targetPercentage >= 0.2 ? fastUpdateInterval : normalUpdateInterval;
    //                yield return new WaitForSeconds(interval);

    //                currentLightRadius = playerLight.pointLightOuterRadius;
    //            }
    //        }
    //    }
    //}
}
