using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class LanternItem: MonoBehaviour, IItem
{
    public int energy;
    ItemInfo info;

    //[HideInInspector]
    //public bool collected = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Get();
        }
    }

    public void Get()
    {
        PlayerManager.Instance.SetPlayerMaxLightEnergy(energy);
        Destroy(gameObject);
    }
}
