using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintSign : MonoBehaviour
{
    [SerializeField]
    private GameObject hintSign;
    [SerializeField]
    private string hintContent;
    [SerializeField]
    private int fontSize;
    [SerializeField]
    private Color32 color;

    private void Start()
    {
        hintSign.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            hintSign.SetActive(true); 

            if (hintSign.transform.GetChild(0).gameObject.TryGetComponent<TMP_Text>(out TMP_Text text))
            {
                text.text = hintContent;
                text.fontSize = fontSize;
                text.color = color;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            hintSign.SetActive(false);
        }
    }
}
