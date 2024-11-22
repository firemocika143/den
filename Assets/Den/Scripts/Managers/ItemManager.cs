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
    [SerializeField]
    private GameObject lanternItemPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void GeneratePageItem(PageItemInfo info, Action record)
    {
        GameObject pageItem = Instantiate(pageItemPrefab, info.pos.position, Quaternion.identity);
        PageItem pageItemScript = pageItem.GetComponent<PageItem>();
        info.gameRecord = record;
        pageItemScript.info = info;
    }

    public void GenerateSkillItem(SkillItemInfo info, Action record)
    {
        GameObject skillItem = Instantiate(skillItemPrefab, info.pos.position, Quaternion.identity);
        SkillItem skillItemScript = skillItem.GetComponent<SkillItem>();
        info.gameRecord = record;
        skillItemScript.info = info;
    }

    public void GenerateLanternItem(LanternItemInfo info)
    {
        GameObject lanternItem = Instantiate(lanternItemPrefab, info.pos.position, Quaternion.identity);
        LanternItem lanternItemScript = lanternItem.GetComponent<LanternItem>();
        lanternItemScript.info = info;
    }
}
