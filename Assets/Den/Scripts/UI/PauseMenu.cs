using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void LoadMenu()
    {
        GameManager.Instance.ResumeGame();
        // TODO - deal with the player
        SceneManager.LoadScene("Menu");
    }

    public void Resume()
    {
        UIManager.Instance.Resume();
    }
}
