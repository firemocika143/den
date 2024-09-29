using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum ladderPart {complete, bottom, top};
    [SerializeField] ladderPart part = ladderPart.complete;
    private GameObject sign;

    private void Start()
    {
        sign.SetActive(false);
    }

    public void TouchLadder()
    {
        sign.SetActive(true);
        PlayerStatus player = sign.GetComponent<PlayerStatus>();
        switch(part)
        {
            case ladderPart.complete:
                player.climb = true;
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
