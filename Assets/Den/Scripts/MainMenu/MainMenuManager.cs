using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void ClickStart()
    {
        //TODO - fade out then load player current scene
        SceneManager.LoadScene("MainScene");
    }

    public void ClickSettings()
    {
        //TODO - set settings panel active and main menu panel inacvtive
    }

    public void ClickQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}