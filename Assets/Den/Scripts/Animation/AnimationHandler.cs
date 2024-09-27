using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    //This script should be the component of objects with animators
    //[SerializeField]
    //private string DefaultState;
    private string currentState;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //ChangeAnimationState(DefaultState);//why this doesn't show the default state wrongly at first?
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        animator.Play(newState);
        currentState = newState;
    }
}
