using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField]
    private AudioSource CurrBGMSource;
    [SerializeField]
    private AudioSource WaitBGMSource;

    [SerializeField]
    private AudioClip Exploring;
    [SerializeField]
    private AudioClip InDanger;
    [SerializeField]
    private AudioClip InLightSource;

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

    public void PlayInDanger()
    {
        WaitBGMSource.clip = InDanger;  
        switchCoroutine = StartCoroutine(SwitchSource(2, 20));
    }

    public void PlayInLightSource()
    {
        WaitBGMSource.clip = InLightSource;
        switchCoroutine = StartCoroutine(SwitchSource(2, 20));
    }

    private IEnumerator SwitchSource(float switchTime, float smoothness)
    {
        AudioSource t = CurrBGMSource;
        CurrBGMSource = WaitBGMSource;
        WaitBGMSource = t;
        CurrBGMSource.Play();

        while (CurrBGMSource.volume < 1 && WaitBGMSource.volume > 0)
        {
            CurrBGMSource.volume += 1 / smoothness;
            WaitBGMSource.volume -= 1 / smoothness;
            yield return new WaitForSeconds(switchTime / smoothness);
        }

        CurrBGMSource.volume = 1;
        WaitBGMSource.volume = 0;
        WaitBGMSource.clip = null;
    }
}
