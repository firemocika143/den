using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTrigger : MonoBehaviour
{
    [SerializeField]
    private SoundManager.ClipEnum EnterClip;
    [SerializeField]
    private SoundManager.ClipEnum LeaveClip;
    [SerializeField]
    private bool replayable = true;

    private bool played = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!played || replayable))
        {
            played = true;
            SoundManager.Instance.ChangeClip(EnterClip);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!played||replayable))
        {
            SoundManager.Instance.ChangeClip(LeaveClip);
        }
    }
}
