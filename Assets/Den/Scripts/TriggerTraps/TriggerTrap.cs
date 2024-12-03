using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerTrap : MonoBehaviour
{
    protected bool solved = false;
    protected bool returning = false;

    [SerializeField]
    protected List<LightTrigger> lightTriggers = new List<LightTrigger>();

    private bool activated = false;

    protected abstract void ActivatedAbility();
    protected abstract void CheckSolve();

    private void Update()
    {
        CheckSolve();

        if (AllTriggered() && !solved && !returning)
        {
            // fail
            returning = true;
            foreach (LightTrigger trigger in lightTriggers)
            {
                trigger.ResetTriggerAfterFailed();
            }
        }
        else if (AllTriggered() && solved && !activated)
        {
            // success
            ActivatedAbility();
            activated = true;

            // TODO - if this trigger trap is reactivatable then start reactivate coroutine to wait for reset
        }
        else if (!AllTriggered() && !AllNotTriggered())
        {
            ResetTimeoutTriggers();
        }

        // this seems to check if all the triggers have returned from shaking
        if (AllWaiting() && returning)
        {
            // handle after fail shake end
            ResetTrap();
            returning = false;
        }
    }

    private bool AllTriggered()
    {
        foreach (LightTrigger trigger in lightTriggers)
        {
            if (!trigger.IsTriggered()) return false;
        }

        return true;
    }

    private bool AllNotTriggered()
    {
        foreach (LightTrigger trigger in lightTriggers)
        {
            if (trigger.IsTriggered()) return false;
        }

        return true;
    }

    private bool AllWaiting()
    {
        foreach (LightTrigger trigger in lightTriggers)
        {
            if (trigger.isShaking) return false;
        }

        return true;
    }

    private void ResetTrap()
    {
        foreach(LightTrigger trigger in lightTriggers)
        { 
            trigger.ResetTrigger(); 
        }

        solved = false;
        returning = false;
        activated = false;
    }

    private void ResetTimeoutTriggers()
    {
        foreach (LightTrigger trigger in lightTriggers)
        {
            trigger.CheckTimeout();
        }
    }
}
