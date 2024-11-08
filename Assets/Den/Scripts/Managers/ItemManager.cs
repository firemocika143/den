using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    [SerializeField]
    private GameObject pageItemPrefab;
    [SerializeField]
    private GameObject skillItemPrefab;

    void Start()
    {
        Instance = this;
    }

    public void GeneratePageItem(Page p, Vector3 worldPos, string hintText, Action gameRecord)
    {
        GameObject pageItem = Instantiate(pageItemPrefab, worldPos, Quaternion.identity);
        PageItem pageItemScript = pageItem.GetComponent<PageItem>();
        pageItemScript.page = p;
        pageItemScript.hintText = hintText;
        pageItemScript.gameRecord = gameRecord;
    }

    public void GenerateSkillItem(Skill sk, Vector3 worldPos, string hintText, Action gameRecord)
    {
        GameObject skillItem = Instantiate(skillItemPrefab, worldPos, Quaternion.identity);
        SkillItem skillItemScript = skillItem.GetComponent<SkillItem>();
        skillItemScript.skill = sk;
        skillItemScript.hintText = hintText;
        skillItemScript.gameRecord = gameRecord;
    }
}
