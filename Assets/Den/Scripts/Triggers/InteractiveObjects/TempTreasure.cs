using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempTreasure : MonoBehaviour
{
    private bool usable = false;
    private bool open = false;

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject sign;

    private void Start()
    {
        sign.SetActive(false);
    }

    private void Update()
    {
        if (usable && !open)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (sign.transform.GetChild(0).TryGetComponent<TextMeshPro>(out TextMeshPro text))
                {
                    text.text = "hurt";
                    text.fontSize = 4;
                }
                playerController.Damage(20);
                open = true;
            }
        }
        else if (usable && open)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (sign.transform.GetChild(0).TryGetComponent<TextMeshPro>(out TextMeshPro text))
                {
                    text.text = "E";
                    text.fontSize = 6;
                }
                playerController.Recover(10);
                open = false;
            }
        }
    }

    // Start is called before the first frame update
    public void Close()
    {
        sign.SetActive(true);
        usable = true;
        Debug.Log("Triggered");
    }

    public void Leave()
    {
        sign.SetActive(false);
        usable = false;
        Debug.Log("Exited");
    }

}
