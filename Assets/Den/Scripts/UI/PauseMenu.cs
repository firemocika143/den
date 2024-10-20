using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public GameObject PauseMenuUI;

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        if (GameManager.Instance.gamePaused)
    //        {
    //            Resume();
    //        }
    //        else
    //        {
    //            Pause();
    //        }
    //    }
    //}

    //public void Resume()
    //{
    //    PauseMenuUI.SetActive(false);
    //    GameManager.Instance.ResumeGame();
    //}

    //void Pause()
    //{
    //    PauseMenuUI.SetActive(true);
    //    GameManager.Instance.PauseGame();
    //}

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
