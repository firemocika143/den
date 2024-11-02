using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    [SerializeField]
    private AnimationHandler anim;

    [Header("Fade In Settings")]
    [SerializeField]
    private string fadeInAnimName;
    public float fadeInTime;

    [Header("Fade Out Settings")]
    [SerializeField]
    private string fadeOutAnimName;
    public float fadeOutTime;

    public void FadeIn()
    {
        anim.ChangeAnimationState(fadeInAnimName);
    }

    public void FadeOut()
    {
        anim.ChangeAnimationState(fadeOutAnimName);
    }
}
