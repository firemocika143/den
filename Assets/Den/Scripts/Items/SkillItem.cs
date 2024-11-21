using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour, IItem// hum... this is actually a light draw item
{
    public SkillItemInfo info;

    private PlayerController pc = null;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //sign.SetActive(true);
            pc = col.GetComponent<PlayerController>();
            Get();
        }
    }

    public void Get()
    {
        GetSkill(info.skill.skillID);
        UIManager.Instance.ShowHint(info.hintText);
        info.gameRecord?.Invoke();
        //if (blocker != null) blocker.SetActive(false);
        Destroy(gameObject);
    }

    private void GetSkill(int skill_id)
    {
        switch (skill_id)
        {
            case 1:
                pc.ObtainLightDraw(); ;
                break;
        }
            
    }
}
