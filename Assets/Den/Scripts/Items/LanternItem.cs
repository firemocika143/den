using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class LanternItem: MonoBehaviour, IItem
{
    public LanternItemInfo info;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Get();
        }
    }

    public void Get()
    {
        UIManager.Instance.ShowHint(info.hintText);
        info.gameRecord?.Invoke();
        PlayerManager.Instance.AddPlayerMaxLightEnergy(info.energy);
        Destroy(gameObject);
    }
}
