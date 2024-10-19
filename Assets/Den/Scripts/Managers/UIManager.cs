using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {get; private set;}

    [SerializeField]
    private PlayerUI playerUI;

    private void Awake()
    {
        Instance = this;
    }
    

    //this seems to be so inefficient
}
