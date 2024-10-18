using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour
{
    [SerializeField]
    private GameObject sign;

    private void Start()
    {
        sign.SetActive(false);
    }

    void OnTriggerEnter2D()
    {
        sign.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (col.TryGetComponent<PlayerAttack>(out PlayerAttack pa))
                {
                    pa.ObtainLightDraw();
                    Destroy(gameObject);
                }
            }
        }
    }

    void OnTriggerExit2D()
    {
        sign.SetActive(false);
    }
}
