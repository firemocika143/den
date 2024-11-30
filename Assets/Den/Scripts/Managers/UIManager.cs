using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using System;

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
    private GameObject DamagePanel;
    [SerializeField]
    private GameObject bookPanel;
    [SerializeField]
    private GameObject hintPanelParent;
    [SerializeField]
    private GameObject hintPanelPrefab;
    [SerializeField]
    private GameObject playerKillTimePanel;

    private List<GameObject> pagePanels = new List<GameObject>();

    private void Start()
    {
        pauseMenuPanel.SetActive(false);
        bookPanel.SetActive(false);
        if (DamagePanel != null) DamagePanel.SetActive(false);
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

            if (bookPanel.activeSelf && pagePanels.Count > 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    FlipToPreviousPage();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    FlipToNextPage();
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

    public void UpdatePlayerPiece(int val)
    {
        if (playerPanel == null || !playerPanel.activeSelf) return;

        if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
        {
            playerUI.UpdatePiece(val);
        }
    }

    //public void UpdatePlayerMaxHealth(int max)
    //{
    //    //if (playerPanel == null || !playerPanel.activeSelf) return;

    //    //if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
    //    //{
    //    //    playerUI.UpdateMaxHealth(max);
    //    //}
    //}
    //public void UpdatePlayerLight(int val)
    //{
    //    //if (playerPanel == null || !playerPanel.activeSelf) return;

    //    //if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
    //    //{
    //    //    playerUI.UpdateLightEnergy(val);
    //    //}
    //}

    //public void UpdatePlayerMaxLight(int max)
    //{
    //    //if (playerPanel == null || !playerPanel.activeSelf) return;

    //    //if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
    //    //{
    //    //    playerUI.UpdateMaxLightEnergy(max);
    //    //}
    //}

    //public void UpdatePlayerAllState(int h_max, int h_val, int l_max, int l_val)
    //{
    //    //if (playerPanel == null || !playerPanel.activeSelf) return;

    //    //if (playerPanel.TryGetComponent<PlayerUI>(out var playerUI))
    //    //{
    //    //    playerUI.UpdateAll(h_max, h_val, l_max, l_val);
    //    //}
    //}

    // Book
    public void OpenBook()
    {
        if (pagePanels.Count != GameManager.Instance.book.pages.Count) ReloadBook(GameManager.Instance.book);

        PlayerManager.Instance.StopPlayer();
        bookPanel.SetActive(true);
        if (pagePanels.Count > 0) pagePanels[GameManager.Instance.currentPage].SetActive(true);// maybe quite inefficient
    }

    public void CloseBook()
    {
        if (pagePanels.Count > 0) pagePanels[GameManager.Instance.currentPage].SetActive(false);// maybe quite inefficient
        bookPanel.SetActive(false);
        PlayerManager.Instance.EnablePlayerToMove();
    }

    public void LoadPage(Page newPage)
    {
        GameObject newPagePanel = Instantiate(newPage.pagePanelPrefab, bookPanel.transform);
        pagePanels.Add(newPagePanel);
        newPagePanel.SetActive(false);

        bookPanel.GetComponent<BookUI>().MoveButtonPanelToTop();
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

    public void FlipToPreviousPage()
    {
        //but I think this will be better to be put in the other scripts like BookUI or something that is on the BookPanel, and maybe we should let the book panel itself to record the page panels
        if (GameManager.Instance.currentPage > 0)
        {
            pagePanels[GameManager.Instance.currentPage].SetActive(false);
            GameManager.Instance.currentPage--;
            pagePanels[GameManager.Instance.currentPage].SetActive(true);
        }
    }

    public void FlipToNextPage()
    {
        if (GameManager.Instance.currentPage < pagePanels.Count - 1)
        {
            pagePanels[GameManager.Instance.currentPage].SetActive(false);
            GameManager.Instance.currentPage++;
            pagePanels[GameManager.Instance.currentPage].SetActive(true);
        }
    }

    // Fade
    public void FadeIn(float waitTime = 0f)
    {
        StopAllCoroutines();
        StartCoroutine(WaitToFadeIn(waitTime));
    }

    private IEnumerator WaitToFadeIn(float t)
    {
        yield return new WaitForSeconds(t);
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Fade>().FadeIn();
    }

    public void FadeOut(Action after = null, float fadeWaitTime = 2f)
    {
        fadePanel.SetActive(true);
        fadePanel.GetComponent<Fade>().FadeOut();
        if (after != null)
        {
            StartCoroutine(FadeWait(fadeWaitTime, after));
        }
    }

    public void FadeMaskOn()
    {
        fadePanel.SetActive(true);
        Image image = fadePanel.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
        // TODO - set the opacity of the mask to 255 (full)
    }

    private IEnumerator FadeWait(float waitTime, Action after = null)
    {
        yield return new WaitForSeconds(waitTime);
        after?.Invoke();
    }

    // Hint
    public void ShowHint(string text)
    {
        GameObject hintPanel = Instantiate(hintPanelPrefab, hintPanelParent.transform);
        TMP_Text t = hintPanel.transform.GetChild(0).GetComponent<TMP_Text>();
        t.text = text;

        AudioSource hintSound = hintPanel.GetComponent<AudioSource>();
        hintSound.Play();  

        PlayableDirector hintTimeline = hintPanel.GetComponent<PlayableDirector>();
        hintTimeline.Play();
        Destroy(hintPanel, (float) hintTimeline.duration);
    }

    // Damage
    public void GetDamage()
    {
        DamagePanel.SetActive(true);
        DamagePanel.GetComponent<AnimationHandler>().ChangeAnimationState("Damaged");
        StartCoroutine(ResetPanel(0.2f, () =>
        {
            DamagePanel.GetComponent<AnimationHandler>().ChangeAnimationState("");
            DamagePanel.SetActive(false);
        }));
    }

    private IEnumerator ResetPanel(float time, Action after)
    {
        yield return new WaitForSeconds(time);
        after?.Invoke();
    }

    public void UpdatePlayerKillTime()
    {
        TMP_Text t = playerKillTimePanel.transform.GetChild(0).GetComponent<TMP_Text>();
        t.text = GameManager.Instance.killTimes.ToString();
        if (GameManager.Instance.killTimes == 0) t.color = Color.yellow;
        else t.color = Color.red;
    }
}
