using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    //[SerializeField]
    //private GameObject SFXPlayerPrefab;
    //[SerializeField]
    //private GameObject SFXBox;

    public AudioSource footStepSFXPlayer;
    public AudioSource getSFXPlayer;
    public AudioSource drawSFXPlayer;

    [Header("Foot Step SFX")]
    [SerializeField]
    private float footStepInterval;
    [SerializeField]
    private float footStepVolumeMultiplier;

    [Header("Get Light Energy SFX")]
    [SerializeField] 
    private float lastFor;
    [SerializeField]
    private float getVolumeMultiplier;

    [Header("Drawing SFX")]
    [SerializeField]
    private float drawInterval;
    [SerializeField]
    private float drawVolumeMultiplier;

    private float footStepTimer = 0;
    private float getTimer = 0;
    private float drawTimer = 0;

    private void Start()
    {
        footStepSFXPlayer.volume *= footStepVolumeMultiplier;
        getSFXPlayer.volume *= getVolumeMultiplier;
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
        if (footStepSFXPlayer.isPlaying) footStepSFXPlayer.Stop();
    }

    public void PlayGetEnergySFX()
    {
        if (!getSFXPlayer.isPlaying) getSFXPlayer.Play();
        getTimer = Time.time;
    }

    public void StopGetSFX()
    {
        if (!getSFXPlayer.isPlaying) return;

        if ((Time.time - getTimer) % getSFXPlayer.clip.length < lastFor) return;
        getSFXPlayer.Stop();
    }

    public void PlayDrawSFX()
    {
        if (Time.time - drawTimer > drawInterval && !drawSFXPlayer.isPlaying)
        {
            drawSFXPlayer.Play();
            drawTimer = Time.time;
        }
    }

    public void StopDrawSFX()
    {
        if (drawSFXPlayer.isPlaying) drawSFXPlayer.Stop();
    }

    //public void PlaySFX(AudioClip clip, float volume)
    //{
    //    GameObject sfxPlayer = Instantiate(SFXPlayerPrefab, SFXBox.transform, false);

    //    AudioSource source = sfxPlayer.GetComponent<AudioSource>();
    //    source.clip = clip;
    //    source.volume = volume;
        
    //    float clipLength = clip.length;
    //    Destroy(sfxPlayer, clipLength);
    //}
}
