using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour, IItem// hum... this is actually a light draw item
{
    public Skill skill;
    [SerializeField]
    private int id;

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
        pc.ObtainLightDraw();
        GameManager.Instance.progress.getLightDraw = true;// and this is very possible to be remake
        //if (blocker != null) blocker.SetActive(false);
        Destroy(gameObject);
    }
}
