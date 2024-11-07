using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StreetLightOff : MonoBehaviour
{
    [SerializeField]
    private StreetFlow flow;//I am not sure if this is good
    [SerializeField]
    private PlayableDirector show;
    [SerializeField]
    private GameObject lightSource;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !triggered)
        {
            triggered = true;
            PlayerController pc = col.gameObject.GetComponent<PlayerController>();
            StartCoroutine(TurnOffShow(pc));
        }
    }

    private IEnumerator TurnOffShow(PlayerController pc)
    {
        pc.StopPlayer();
        show.Play();
        SoundManager.Instance.ChangeClip(SoundManager.Instance.clips.DANGER);
        yield return new WaitForSeconds(10f);
        GameManager.Instance.progress.finishLightOff = true;
        pc.state.stop = false;
        lightSource.SetActive(false);
    }
}
