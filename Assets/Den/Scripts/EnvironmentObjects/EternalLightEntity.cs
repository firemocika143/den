using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EternalLightEntity : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector timeline;

    public void Get()
    {
        GameManager.Instance.progress.getEternalLightEntity = true;
        PlayerManager.Instance.StopPlayer();
        timeline.Play();
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds((float) timeline.duration);
        PlayerManager.Instance.EnablePlayerToMove();
        Destroy(gameObject);
    }
}
