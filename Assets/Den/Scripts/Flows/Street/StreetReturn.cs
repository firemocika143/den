using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetReturn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //this way doesn't seem to be good
            col.gameObject.GetComponent<PlayerController>().InstantDie();
            StreetFlow sf = (StreetFlow)GameManager.Instance.flow;// and this looks really bad
            StartCoroutine(sf.ReturnFlow());
        }
    }
}
