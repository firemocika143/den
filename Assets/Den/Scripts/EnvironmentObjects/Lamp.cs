using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    [SerializeField]
    private AnimationHandler animHandler;

    public bool went = false;

    public void Off()
    {
        animHandler.ChangeAnimationState("Off");
    }

    public void LightUp()
    {
        animHandler.ChangeAnimationState("LightUp");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            went = true;
        }
    }
}
