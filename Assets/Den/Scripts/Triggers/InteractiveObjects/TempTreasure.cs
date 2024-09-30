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
                    text.text = "Stop";
                    text.fontSize = 4;
                }
                playerController.state.stop = true;
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
                playerController.state.stop = false;
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
