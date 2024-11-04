using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetUpdate : MonoBehaviour
{
    [SerializeField]
    private int updateNumber;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            LampManager.Instance.UpdateReopen(updateNumber);
        }
    }
}
