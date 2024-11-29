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
        //TODO - hit boss for 20 damage and let it down for 3 seconds
    }
}
