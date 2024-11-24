using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Ending : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector endTimeline;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.Instance.ChangeClip(SoundManager.ClipEnum.NULL);
            endTimeline.Play();
        }
    }
}
