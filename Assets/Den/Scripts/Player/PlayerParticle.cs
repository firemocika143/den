using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem ps;
    [SerializeField]
    private float lastFor;

    //private float timer;

    public void PlayRecoverParticle()
    {
        if (!ps.isPlaying)
        {
            ps.Play();

            var main = ps.main;// Why it seems I can only call it main?
            main.loop = true;
        }
    }

    public void StopRecoverParticle()
    {
        var main = ps.main;
        main.loop = false;
        //ps.Stop(true);
    }
}
