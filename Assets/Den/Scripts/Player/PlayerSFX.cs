using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    //public AudioClip footStep;
    //public AudioClip lightOn;
    //public AudioClip drawStart;

    //public AudioSource footStepSFXPlayer;

    //public float FootStepInterval;

    //private IEnumerator footStepSFXCoroutine;

    //private void Start()
    //{
    //    footStepSFXCoroutine = FootStepSFXCoroutine();
    //}

    //public void PlayFootStepSFX()
    //{
    //    if (footStepSFXCoroutine == null)
    //    {
    //        Debug.LogError("didn't set up the IEnumerator for footstep");
    //        footStepSFXCoroutine = FootStepSFXCoroutine();
    //    }

    //    StopAllCoroutines();
    //    StartCoroutine(footStepSFXCoroutine);
    //}

    //public void PauseFootStepSFX()
    //{
    //    footStepSFXPlayer.Pause();
    //    StopAllCoroutines();
    //}

    //private IEnumerator FootStepSFXCoroutine()
    //{
    //    while (true)
    //    {
    //        footStepSFXPlayer.Play();
    //        yield return new WaitForSeconds(FootStepInterval);
    //    }
    //}

    public AudioSource footStepSFXPlayer;

    [SerializeField]
    private float footStepInterval;
    [SerializeField]
    private float footStepVolumeMultiplier;
    
    private float footStepTimer = 0;

    private void Start()
    {
        footStepSFXPlayer.volume *= footStepVolumeMultiplier;
    }

    public void PlayFootStepSFX()
    {
        if (Time.time - footStepTimer > footStepInterval && !footStepSFXPlayer.isPlaying)
        {
            footStepSFXPlayer.Play();
            footStepTimer = Time.time;
        }
    }

    public void StopFootStepSFX()
    {
        footStepSFXPlayer.Stop();
    }
}
