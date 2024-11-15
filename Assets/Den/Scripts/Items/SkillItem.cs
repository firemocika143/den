using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour, IItem// hum... this is actually a light draw item
{
    public Skill skill;
    public string hintText;

    public Action gameRecord;

    public ItemInfo info;

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
        GetSkill(skill.skillID);
        UIManager.Instance.ShowHint(hintText);
        gameRecord?.Invoke();
        //if (blocker != null) blocker.SetActive(false);
        Destroy(gameObject);
    }

    private void GetSkill(int skill_id)
    {
        switch (skill_id)
        {
            case 0:
                pc.GetLantern();
                break;
            case 1:
                GameManager.Instance.progress.getLightDraw = true;// and this is very possible to be remake
                pc.ObtainLightDraw(); ;
                break;
        }
            
    }
}
