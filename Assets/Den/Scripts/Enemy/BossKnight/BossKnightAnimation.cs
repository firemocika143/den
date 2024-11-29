using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossKnightAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationHandler animHandler;
    [SerializeField]
    private Rigidbody2D rb;//temp

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x != 0)
        {
            animHandler.ChangeAnimationState("Walk");
        }
        else
        {
            animHandler.ChangeAnimationState("Idle");
        }
    }
}
