using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StreetLightOff : MonoBehaviour
{
    [SerializeField]
    private StreetFlow flow;//I am not sure if this is good
    [SerializeField]
    private PlayableDirector firstTimeShow;
    [SerializeField]
    private PlayableDirector otherTimeShow;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !triggered)
        {
            triggered = true;
            flow.isInLightOffEvent = true;
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();
            SoundManager.Instance.ChangeClip(SoundManager.Instance.clips.DANGER);
            StartCoroutine(TurnOffShow(pc));
        }
    }

    private IEnumerator TurnOffShow(PlayerController pc)
    {
        pc.StopPlayer();
        if (GameManager.Instance.progress.firstTimeLightOff)
        {
            firstTimeShow.Play();
            yield return new WaitForSeconds(10f);
        }
        else otherTimeShow.Play();

        GameManager.Instance.progress.firstTimeLightOff = false;
        pc.state.stop = false;
    }
}
