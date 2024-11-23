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
        public AudioClip MEMORY;
    }

    [System.Serializable]
    public enum ClipEnum
    {
        EXPLORING, 
        DANGER,
        STREETLIGHTSOURCE,
        LIBRARYLIGHTSOURCE,
        NULL,
        MEMORY,
    }

    public Clips clips;

    private float vMultiplier = 1;

    private Coroutine switchCoroutine;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        BGMSource.volume = 1f;
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

    public void ChangeClip(AudioClip ac, float switchTime = 3f, bool loop = true)
    {
        StopAllCoroutines();

        if (BGMSource != null)
        {
            BGMSource.loop = loop;
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

    public void ChangeClip(ClipEnum ac_name, bool loop = true)
    {
        AudioClip ac = EnumToAC(ac_name);
        ChangeClip(ac, 3f, loop);
    }

    private IEnumerator FadeOut(float switchTime, Action after_fade_out = null)
    {
        while (fadePercentage > 0)
        {
            BGMSource.volume = Mathf.Lerp(0, defaultVolume * vMultiplier, fadePercentage);
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
            BGMSource.volume = Mathf.Lerp(0, defaultVolume * vMultiplier, fadePercentage);
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
            case ClipEnum.MEMORY:
                return clips.MEMORY;
            case ClipEnum.NULL:
                return null;
        }

        Debug.LogError("No this clip");
        return null;
    }

    public void SetVolume(float multiplier_val)
    {
        vMultiplier = multiplier_val;
        if (BGMSource.isPlaying)
        {
            BGMSource.volume = defaultVolume * vMultiplier;
        }
    }

    public void SetBackVolume()
    {
        vMultiplier = 1;
        BGMSource.volume = defaultVolume * vMultiplier;
    }
}
