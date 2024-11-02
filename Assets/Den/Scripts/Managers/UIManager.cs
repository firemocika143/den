using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private GameObject pagePanel;
    [SerializeField]
    private GameObject pauseMenuPanel;
    [SerializeField]
    private GameObject playerPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.gamePaused && pauseMenuPanel.activeSelf) Resume();
            else Pause();
        }
    }

    public void ReadPage(PageItem.Page p)
    {
        GameManager.Instance.PauseGame();
        pagePanel.SetActive(true);
        if (pagePanel.TryGetComponent<PageUI>(out var u))
        {
            u.LoadPage(p);
        }
    }

    public void ClosePage()
    {
        pagePanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    private void Pause()
    {
        pauseMenuPanel.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        if (!pagePanel.activeSelf) GameManager.Instance.ResumeGame();
    }

    public bool PauseMenuOpened()
    {
        return pauseMenuPanel.activeSelf; 
    }

    public void UpdatePlayerHealth(int val)
    {
        if (playerPanel == null || !playerPanel.activeSelf) return;

        if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
        {
            playerUI.UpdateHealth(val);
        }
    }

    public void UpdatePlayerMaxHealth(int max)
    {
        if (playerPanel == null || !playerPanel.activeSelf) return;

        if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
        {
            playerUI.UpdateMaxHealth(max);
        }
    }
    public void UpdatePlayerLight(int val)
    {
        if (playerPanel == null || !playerPanel.activeSelf) return;

        if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
        {
            playerUI.UpdateLightEnergy(val);
        }
    }

    public void UpdatePlayerMaxLight(int max)
    {
        if (playerPanel == null || !playerPanel.activeSelf) return;

        if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
        {
            playerUI.UpdateMaxLightEnergy(max);
        }
    }

    public void UpdatePlayerAllState(int h_max, int h_val, int l_max, int l_val)
    {
        if (playerPanel == null || !playerPanel.activeSelf) return;

        if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
        {
            playerUI.UpdateAll(h_max, h_val, l_max, l_val);
        }
    }
}
