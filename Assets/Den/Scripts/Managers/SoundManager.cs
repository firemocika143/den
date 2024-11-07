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

    public Clips clips;

    private Coroutine switchCoroutine;

    private void Start()
    {
        Instance = this;

        CurrBGMSource.volume = 1f;
        WaitBGMSource.volume = 0f;

        CurrBGMSource.Pause();
        WaitBGMSource.Pause();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void ResetBGM()
    {
        CurrBGMSource.clip = null;
        WaitBGMSource.clip = null;

        CurrBGMSource.Pause();
        WaitBGMSource.Pause();
    }

    public void ChangeClip(AudioClip ac)
    {
        if (CurrBGMSource.clip == ac) return;
        if (CurrBGMSource.clip == null)
        {
            CurrBGMSource.clip = ac;
            CurrBGMSource.Play();
            return;
        }

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

        CurrBGMSource.Stop();
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
}
