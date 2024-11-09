using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMTrigger : MonoBehaviour
{
    [SerializeField]
    private SoundManager.ClipEnum EnterClip;
    [SerializeField]
    private SoundManager.ClipEnum LeaveClip;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.Instance.ChangeClip(EnterClip);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SoundManager.Instance.ChangeClip(LeaveClip);
        }
    }
}
