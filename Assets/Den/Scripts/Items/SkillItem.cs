using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour, IItem
{
    public Skill skill;

    [SerializeField]
    private GameObject sign;
    [SerializeField]
    private GameObject blocker;//ugh... this is weird
    [SerializeField]
    private int id;

    private PlayerController pc = null;

    private void Start()
    {
        // this check should be in StreetController or StreetFlow
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
        if (blocker != null) blocker.SetActive(true);
    }

    private void Update()
    {
        if (pc != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Get();
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

    public void Get()
    {
        pc.ObtainLightDraw();
        GameManager.Instance.progress.getLightDraw = true;// and this is very possible to be remake
        if (blocker != null) blocker.SetActive(false);
        Destroy(gameObject);
    }
}
