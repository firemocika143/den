using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookUI : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonPanel;

    public void MoveButtonPanelToTop()
    {
        ButtonPanel.transform.SetSiblingIndex(ButtonPanel.transform.GetSiblingIndex() + 1);
    }
}
