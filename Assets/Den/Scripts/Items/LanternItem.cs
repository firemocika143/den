using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class LanternItem: IItem
{
    public int energy = 5;
    public Transform pos;
    public Skill skill;
    public string hintText;
    public Action gameRecord;

    [HideInInspector]
    public bool collected = false;

    public void Get()
    {

    }
}
