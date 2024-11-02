using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    [SerializeField]
    private GameObject sign;
    [SerializeField]
    private GameObject tutorialSign;
    [SerializeField]
    private GameObject blocker;//ugh... this is weird
    [SerializeField]
    private int id;

    private PlayerController pc = null;

    private void Start()
    {
        pc = FindFirstObjectByType<PlayerController>();//ugh... it's a little bit too much 
        if (pc != null)
        {
            if (id == 0 && pc.state.getLightDraw)
            {
                blocker.SetActive(false);
                Destroy(this.gameObject);
            }
        }

        pc = null;
        sign.SetActive(false);
        tutorialSign.SetActive(false);
        if (blocker != null) blocker.SetActive(true);
    }

    private void Update()
    {
        if (pc != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pc.ObtainLightDraw();
                tutorialSign.SetActive(true);
                if (blocker != null) blocker.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(true);
            pc = col.GetComponent<PlayerController>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(false);
            pc = null;
        }
    }
}
