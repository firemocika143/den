using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private string openGateAnimationName;

    public void OpenGate()
    {
        GetComponent<AnimationHandler>().ChangeAnimationState(openGateAnimationName);
        GetComponent<AudioSource>().Play();
    }
}
