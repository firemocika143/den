using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SequencingTriggerTrap : TriggerTrap
{
    private List<int> triggeredSeq;

    protected override void CheckSolve()
    {
        for (int i = 0; i < lightTriggers.Count; i++)
        {
            if (lightTriggers[i].IsTriggered())
            {
                if (!triggeredSeq.Contains(i))
                {
                    triggeredSeq.Add(i);
                }
            }
        }

        if (triggeredSeq.Count != lightTriggers.Count)
        {
            solved = false;
            return;
        }
        for (int i = 0; i < triggeredSeq.Count; i++)
        {
            if (triggeredSeq[i] != i)
            {
                solved = false;
                triggeredSeq.Clear();
                return;
            }
        }

        solved = true;
        return;
    }
}
