using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public float defaultVolume = 1f;

    [SerializeField]
    private AudioSource CurrBGMSource;
    [SerializeField]
    private AudioSource WaitBGMSource;

    [System.Serializable]
    public class Clips
    {
        public AudioClip EXPLORING;
        public AudioClip DANGER;
        public AudioClip STREETLIGHTSOURCE;
        public AudioClip LIBRARYLIGHTSOURCE;
    }
    //public AudioClip Exploring;
    //[SerializeField]
    //private AudioClip InDanger;
    //[SerializeField]
    //private AudioClip StreetLightSource;
    //[SerializeField]
    //private AudioClip LibraryLightSource;

    public Clips clips;


    private Coroutine switchCoroutine;

    private void Start()
    {
        Instance = this;

        CurrBGMSource.volume = 1f;
        WaitBGMSource.volume = 0f;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void ResetSound()
    {
        CurrBGMSource.clip = null;
        WaitBGMSource.clip = null;
    }

    //public void PlayInDanger()
    //{
    //    if (CurrBGMSource.clip == clips.DANGER) return;

    //    WaitBGMSource.clip = clips.DANGER;  
    //    switchCoroutine = StartCoroutine(SwitchSource(2, 20));
    //}

    //public void PlayStreetLightSource()
    //{
    //    if (CurrBGMSource.clip == clips.STREETLIGHTSOURCE) return;

    //    WaitBGMSource.clip = clips.STREETLIGHTSOURCE;
    //    switchCoroutine = StartCoroutine(SwitchSource(5, 20));
    //}
    //public void PlayLibraryLightSource()
    //{
    //    if (CurrBGMSource.clip == clips.LIBRARYLIGHTSOURCE) return;

    //    WaitBGMSource.clip = clips.LIBRARYLIGHTSOURCE;
    //    switchCoroutine = StartCoroutine(SwitchSource(5, 20));
    //}

    public void ChangeClip(AudioClip ac)
    {
        if (CurrBGMSource.clip == ac) return;

        StopAllCoroutines();
        WaitBGMSource.clip = ac;
        StartCoroutine(SwitchBGM(3f));
    }

    private IEnumerator SwitchBGM(float switchTime)
    {
        float percentage = 0;
        while (CurrBGMSource.volume > 0)
        {
            CurrBGMSource.volume = Mathf.Lerp(defaultVolume, 0, percentage);
            percentage += Time.deltaTime / switchTime;
            yield return null;
        }

        CurrBGMSource.Pause();
        if (WaitBGMSource.isPlaying == false)
            WaitBGMSource.Play();
        WaitBGMSource.UnPause();
        percentage = 0;

        while (WaitBGMSource.volume < defaultVolume)
        {
            WaitBGMSource.volume = Mathf.Lerp(0, defaultVolume, percentage);
            percentage += Time.deltaTime / switchTime;
            yield return null;
        }

        AudioSource t = CurrBGMSource;
        CurrBGMSource = WaitBGMSource;
        WaitBGMSource = t;
    }

    //private IEnumerator SwitchSource(float switchTime, float smoothness)
    //{
    //    AudioSource t = CurrBGMSource;
    //    CurrBGMSource = WaitBGMSource;
    //    WaitBGMSource = t;
    //    CurrBGMSource.Play();

    //    while (CurrBGMSource.volume < 1 && WaitBGMSource.volume > 0)
    //    {
    //        CurrBGMSource.volume += 1 / smoothness;
    //        WaitBGMSource.volume -= 1 / smoothness;
    //        yield return new WaitForSeconds(switchTime / smoothness);
    //    }

    //    CurrBGMSource.volume = 1;
    //    WaitBGMSource.volume = 0;
    //    WaitBGMSource.clip = null;
    //}
}
