using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkKnightAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationHandler animHandler;
    [SerializeField]
    private Rigidbody2D rb;//temp
    [SerializeField]
    private BossDarkKnight bossScript;

    // Update is called once per frame
    void Update()
    {
        if(bossScript.readyToAttack)
        {
            animHandler.ChangeAnimationState("ReadyToAttack");
        }
        else if (bossScript.usingAttack1)
        {
            animHandler.ChangeAnimationState("Attack1");
        }
        else if (rb.velocity.x != 0)
        {
            animHandler.ChangeAnimationState("Walk");
        }
        else
        {
            animHandler.ChangeAnimationState("Idle");
        }
    }
}
