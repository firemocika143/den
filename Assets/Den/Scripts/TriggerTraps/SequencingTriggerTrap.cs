using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SequencingTriggerTrap : TriggerTrap
{
    [SerializeField]
    private List<int> triggeredSeq = new List<int>();
    [SerializeField]
    private int[] triggeredSeq2 = new int[5];

    protected override void CheckSolve()
    {
        for (int i = 0; i < lightTriggers.Count; i++)
        {
            if (returning) return;
            if (lightTriggers[i].IsTriggered())
            {
                if (!triggeredSeq.Contains(i))
                {
                    triggeredSeq.Add(i);
                }
            }
            else if (!lightTriggers[i].IsTriggered())
            {
                if (triggeredSeq.Contains(i))
                {
                    triggeredSeq.Remove(i);
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
                for (int j = 0; j < triggeredSeq.Count; j++)
                {
                    Debug.Log(triggeredSeq[j]);
                }
                Debug.Log("Failed");
                return;
            }
        }

        solved = true;
        return;
    }
}
