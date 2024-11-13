using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private float lastFor;

    private float timer;

    public void PlayRecoverParticle()
    {
        if (!particleSystem.isPlaying) particleSystem.Play();
        timer = Time.time;
    }
    public void StopRecoverParticle()
    {
        if (!particleSystem.isPlaying) return;

        if ((Time.time - timer) % particleSystem.main.duration < lastFor) return;
        particleSystem.Stop();
    }
}
