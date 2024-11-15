using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ItemInfo
{
    public string hintText;
    public Transform pos;
    [HideInInspector]
    public Action gameRecord = null;
}
