using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneSeqTrap : SequencingTriggerTrap
{
    [SerializeField]
    private PlayableDirector timeline;
    [SerializeField]
    private SoundManager.ClipEnum clip;

    protected override void ActivatedAbility()
    {
        timeline.Play();
        SoundManager.Instance.ChangeClip(clip);
    }
}
