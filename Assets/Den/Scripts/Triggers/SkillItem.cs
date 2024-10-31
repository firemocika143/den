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
    private GameObject blocker;

    private PlayerController pc = null;

    private void Start()
    {
        sign.SetActive(false);
        tutorialSign.SetActive(false);
        blocker.SetActive(true);
    }

    private void Update()
    {
        if (pc != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pc.ObtainLightDraw();
                tutorialSign.SetActive(true);
                blocker.SetActive(false);
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
