using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    //[SerializeField]
    //private BarHandler healthBar;
    //[SerializeField]
    //private BarHandler lightEnergyBar;

    [SerializeField]
    private TMP_Text health_amount;
    [SerializeField]
    private TMP_Text piece_amount;

    public void UpdateHealth(int val)
    {
        health_amount.text = val.ToString();
    }

    public void UpdatePiece(int val)
    {
        piece_amount.text = val.ToString();
    }

    //public void UpdateMaxHealth(int max)
    //{
    //    healthBar.SetMax(max);
    //}

    //public void UpdateLightEnergy(int val)
    //{
    //    lightEnergyBar.SetValue(val);
    //}

    //public void UpdateMaxLightEnergy(int max)
    //{
    //    lightEnergyBar.SetMax(max);
    //}

    //public void UpdateAll(int m_h, int h, int m_l, int l)
    //{
    //    healthBar.SetMax(m_h);
    //    healthBar.SetValue(h);
    //    lightEnergyBar.SetMax(m_l);
    //    lightEnergyBar.SetValue(l);
    //}
}
