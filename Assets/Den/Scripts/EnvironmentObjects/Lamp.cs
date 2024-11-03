using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    private AnimationHandler animHandler;
    [SerializeField]
    private string off;

    public void Off()
    {
        animHandler.ChangeAnimationState(off);
    }
}
