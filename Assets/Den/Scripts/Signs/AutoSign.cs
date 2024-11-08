using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSign : MonoBehaviour
{
    [SerializeField]
    private GameObject sign;

    private void Start()
    {
        sign.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            sign.SetActive(false);
        }
    }
}
