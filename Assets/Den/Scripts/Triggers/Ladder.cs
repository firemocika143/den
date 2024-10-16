using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum ladderPart {complete, bottom, top};
    [SerializeField] 
    private ladderPart part = ladderPart.complete;
    [SerializeField]
    private GameObject sign;

    private void Start()
    {
        sign.SetActive(false);
    }

    public void TouchLadder()
    {
        sign.SetActive(true);
        PlayerController playerController = sign.GetComponent<PlayerController>();
        switch(part)
        {
            case ladderPart.complete:
                playerController.state.climb = true;
                break;
            case ladderPart.bottom:
                break;
            case ladderPart.top:
                break;
            default:
                break;
        }
        Debug.Log("Triggered (Ladder)");
    }

    public void LeaveLadder()
    {
        sign.SetActive(false);
        Debug.Log("Untriggered (Ladder)");  
    }
}
