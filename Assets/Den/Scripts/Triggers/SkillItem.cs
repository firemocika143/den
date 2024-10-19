using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    [SerializeField]
    private GameObject sign;

    private PlayerAttack pa = null;

    private void Start()
    {
        sign.SetActive(false);
    }

    private void Update()
    {
        if (pa != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pa.ObtainLightDraw();
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(true);
            pa = col.GetComponent<PlayerAttack>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(false);
            pa = null;
        }
    }
}
