using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkKnightDarkTrigger : NonSeqTriggerTrap
{
    [Header("Boss Dark Knight")]
    public BossDarkKnight bossDarkKnight;

    protected override void ActivatedAbility()
    {
        bossDarkKnight.Skill3Triggered();
    }
}
