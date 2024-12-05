using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string nextSceneName;
    public bool triggered = false;
    public PlayableDirector pd;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !triggered)
        {
            //LoadSceneMode scene after a light
            DataPersistenceManager.instance.SaveGame();
            GameManager.Instance.playerFirmPiece += PlayerManager.Instance.playerLanternPiece;
            //GameManager.Instance.CurrScene = nextSceneName;
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        UIManager.Instance.FadeOut();
        // PlaySFX?
        yield return new WaitForSeconds(3f);
        pd.Play();
        yield return new WaitForSeconds((float)pd.duration);
        SceneManager.LoadScene(nextSceneName);
    }
}
