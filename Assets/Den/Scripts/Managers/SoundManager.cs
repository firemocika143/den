using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public float defaultVolume = 1f;

    [SerializeField]
    private AudioSource BGMSource;

    private float fadePercentage = 1;

    [System.Serializable]
    public class Clips
    {
        public AudioClip EXPLORING;
        public AudioClip DANGER;
        public AudioClip STREETLIGHTSOURCE;
        public AudioClip LIBRARYLIGHTSOURCE;
    }

    [System.Serializable]
    public enum ClipEnum
    {
        EXPLORING, 
        DANGER,
        STREETLIGHTSOURCE,
        LIBRARYLIGHTSOURCE
    }

    public Clips clips;

    private Coroutine switchCoroutine;

    private void Start()
    {
        Instance = this;

        BGMSource.volume = 1f;

        BGMSource.Play();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void ResetBGM()
    {
        BGMSource.clip = null;

        BGMSource.Stop();
    }

    public void ChangeClip(AudioClip ac, float switchTime = 3f)
    {
        StopAllCoroutines();

        if (BGMSource != null)
        {
            if (BGMSource.clip == null)
            {
                Debug.Log("last clip is null");
                BGMSource.clip = ac;
                fadePercentage = 0;
                StartCoroutine(FadeIn(switchTime));
            }
            else if (BGMSource.clip == ac)
            {
                StartCoroutine(FadeIn(switchTime));
            }
            else
            {
                StartCoroutine(FadeOut(switchTime, () =>
                {
                    BGMSource.clip = ac;
                    StartCoroutine(FadeIn(switchTime));
                }));
            }
        }
        else
        {
            Debug.LogError("no sound source");
        }
    }

    public void ChangeClip(ClipEnum ac_name)
    {
        AudioClip ac = EnumToAC(ac_name);
        ChangeClip(ac);
    }

    private IEnumerator FadeOut(float switchTime, Action after_fade_out = null)
    {
        while (fadePercentage > 0)
        {
            BGMSource.volume = Mathf.Lerp(0, defaultVolume, fadePercentage);
            fadePercentage -= Time.deltaTime / switchTime;
            yield return null;
        }
        BGMSource.volume = 0;
        fadePercentage = 0;
        BGMSource.Stop();

        after_fade_out?.Invoke();
    }

    private IEnumerator FadeIn(float switchTime, Action after_fade_in = null)
    {
        if (!BGMSource.isPlaying) BGMSource.Play();
        while (fadePercentage < 1)
        {
            BGMSource.volume = Mathf.Lerp(0, defaultVolume, fadePercentage);
            fadePercentage += Time.deltaTime / switchTime;
            yield return null;
        }
        BGMSource.volume = 1;
        fadePercentage = 1;

        after_fade_in?.Invoke();
    }

    private AudioClip EnumToAC(ClipEnum ac_name)
    {
        switch (ac_name)
        {
            case ClipEnum.EXPLORING:
                return clips.EXPLORING;
            case ClipEnum.LIBRARYLIGHTSOURCE:
                return clips.LIBRARYLIGHTSOURCE;
            case ClipEnum.STREETLIGHTSOURCE:
                return clips.STREETLIGHTSOURCE;
            case ClipEnum.DANGER:
                return clips.DANGER;
        }

        Debug.LogError("No this clip");
        return null;
    }
}
