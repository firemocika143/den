using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    [SerializeField]
    private GameObject sign;

    private PlayerController pc = null;

    private void Start()
    {
        sign.SetActive(false);
    }

    private void Update()
    {
        if (pc != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pc.ObtainLightDraw();
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
