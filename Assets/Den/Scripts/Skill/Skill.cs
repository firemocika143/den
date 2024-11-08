using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public int skillID;

    public Skill(string skillName, int skillID)
    {
        this.skillName = skillName;
        this.skillID = skillID;
    }

}
