using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOffEnd : MonoBehaviour
{
    [SerializeField]
    private StreetFlow sf;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !triggered)
        {
            triggered = true;
            GameManager.Instance.progress.finishLightOff = true;
            sf.isInLightOffEvent = false;
            SoundManager.Instance.ChangeClip(SoundManager.ClipEnum.EXPLORING);
        }
    }
}
