using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private GameObject pauseMenuPanel;
    [SerializeField]
    private GameObject playerPanel;
    [SerializeField]
    private GameObject fadePanel;
    [SerializeField]
    private GameObject bookPanel;

    private List<GameObject> pagePanels = new List<GameObject>();

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
        fadePanel.SetActive(false);
        bookPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.gamePaused && pauseMenuPanel.activeSelf) Resume();
            else Pause();
        }

        if (!pauseMenuPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                // should I make this works only if the player is in light source?
                if (!bookPanel.activeSelf) OpenBook();
                else CloseBook();
            }

            if (bookPanel.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if (GameManager.Instance.currentPage > 0)
                    {
                        pagePanels[GameManager.Instance.currentPage].SetActive(false);
                        GameManager.Instance.currentPage--;
                        pagePanels[GameManager.Instance.currentPage].SetActive(true);
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if (GameManager.Instance.currentPage < pagePanels.Count - 1)
                    {
                        pagePanels[GameManager.Instance.currentPage].SetActive(false);
                        GameManager.Instance.currentPage++;
                        pagePanels[GameManager.Instance.currentPage].SetActive(true);
                    }
                }
            }
        }
    }

    // Pause
    private void Pause()
    {
        pauseMenuPanel.SetActive(true);
        GameManager.Instance.PauseGame();
    }

    public void Resume()
    {
        pauseMenuPanel.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public bool PauseMenuOpened()
    {
        return pauseMenuPanel.activeSelf; 
    }

    // Player
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

    // Book
    public void OpenBook()
    {
        if (pagePanels.Count != GameManager.Instance.book.pages.Count) ReloadBook(GameManager.Instance.book);
        bookPanel.SetActive(true);
        pagePanels[GameManager.Instance.currentPage].SetActive(true);// maybe quite inefficient
    }

    public void CloseBook()
    {
        pagePanels[GameManager.Instance.currentPage].SetActive(false);// maybe quite inefficient
        bookPanel.SetActive(false);
    }

    public void LoadPage(Page newPage)
    {
        GameObject newPagePanel = Instantiate(newPage.pagePanelPrefab, bookPanel.transform);
        pagePanels.Add(newPagePanel);
        newPagePanel.SetActive(false);
    }

    public void LoadBook(Book book)
    {
        // TODO - show something to tell the player that the book is on loading
        foreach (Page p in book.pages)
        {
            LoadPage(p);
        }
    }

    public void ReloadBook(Book book)
    {
        // nah... If I can record each page on each page panel, I wouldn't need to delete all of them
        foreach (var pagePanel in pagePanels)
        {
            Destroy(pagePanel);
        }
        pagePanels.Clear();

        LoadBook(book);
    }

    // Fade
    public void FadeIn()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Fade>().FadeIn();
    }

    public void FadeOut()
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Fade>().FadeOut();
    }

    public void FadeMaskOn()
    {
        fadePanel.SetActive(true);
        // TODO - set the opacity of the mask to 255 (full)
    }
}
