using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetController : MonoBehaviour
{
    // TODO - set player respawn point and items in the streeet, etc

    [SerializeField]
    private ItemManager itemManager;

    [SerializeField]
    private Page page1;

    void Start()
    {
        if (!GameManager.Instance.progress.finishLightOff)
        {
            
            return;
        }


    }
}
