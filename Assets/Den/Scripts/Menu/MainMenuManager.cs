using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance { get; private set; }

    public GameObject mainMenu;
    public GameObject settingsPanel;

    private void Start()
    {
        Instance = this;
    }

    public void ClickStart()
    {
        //TODO - fade out then load player current scene
        SceneManager.LoadScene(GameManager.Instance.flow.name);
    }

    public void ClickSettings()
    {
        //TODO - set settings panel active and main menu panel inacvtive
        settingsPanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        // why would this be here
        settingsPanel.SetActive(false);
        mainMenu.SetActive(true);
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