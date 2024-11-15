using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemInfo
{
    public string hintText;
    public Action gameRecord;
    public Transform pos;

    public ItemInfo(Transform pos, string hintText = null, Action gameRecord = null)
    {
        this.pos = pos;
        this.hintText = hintText;
        this.gameRecord = gameRecord;
    }
}
