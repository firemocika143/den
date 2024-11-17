using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class LampDevice : Device
{
    private void Start()
    {
        StartReset();
        LampStartSetting();
    }

    private void Update()
    {
        UpdateCheck();
        LampUpdate();
    }

    public abstract void LampStartSetting();
    public abstract void LampUpdate();
}
