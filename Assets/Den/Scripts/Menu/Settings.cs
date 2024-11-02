using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject settingsPanel;
    // key settings
    // return
    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void Return()
    {
        MainMenuManager.Instance.CloseSettings();
    }
}
