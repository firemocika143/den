using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy: MonoBehaviour
{
    protected bool invincible = false;
    protected float invincibleTime = 0.3f;

    public abstract void Damage(int d);
    public abstract void Spawn();
    public abstract void Kill();

    protected abstract void HitFlash(Action after = null);

    public IEnumerator InvincibleTimeCount()
    {
        invincible = true;

        yield return new WaitForSeconds(invincibleTime);

        invincible = false;
    }
}
