using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private float lastFor;

    //private float timer;

    public void PlayRecoverParticle()
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();

            var main = particleSystem.main;// Why it seems I can only call it main?
            main.loop = true;
        }
    }

    public void StopRecoverParticle()
    {
        var main = particleSystem.main;
        main.loop = false;
    }
}
