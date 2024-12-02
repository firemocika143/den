using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonSeqTriggerTrap : TriggerTrap
{
    protected override void CheckSolve()
    {
        foreach(LightTrigger lightTrigger in lightTriggers)
        {
            if (!lightTrigger.IsTriggered())
            {
                solved = false;
                return;
            }
        }

        solved = true;
        return;
    }
}
