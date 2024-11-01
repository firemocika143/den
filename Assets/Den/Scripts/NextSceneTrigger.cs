using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //LoadSceneMode scene after a light
            DataPersistenceManager.instance.SaveGame();
            GameManager.Instance.currScene = sceneName;
            SceneManager.LoadScene(sceneName);
        }
    }
}
