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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.gamePaused) Resume();
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
        GameManager.Instance.ResumeGame();
        pagePanel.SetActive(false);
    }

    private void Pause()
    {
        pauseMenuPanel.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    private void Resume()
    {
        pauseMenuPanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
}
