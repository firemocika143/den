using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class test : MonoBehaviour
{
    public Transform targetPoint;
    public Transform idlePoint;
    public PlayerController pc;
    private AIDestinationSetter ai;

    private bool chase;
    private bool safe;
    private string playerTag;
    private string lightSourceTag;

    void Start()
    {
        ai = GetComponent<AIDestinationSetter>();
        chase = false;
        safe = true;
        playerTag = "Player";
        lightSourceTag = "LightSorce";
    }
    
    void Update()
    {
        if (chase)
        {
            if (ai.target != null)
            {
                if (pc.isInLightSource)
                {
                    if (safe)
                    {
                        ai.target = transform;
                    }
                    else
                    {
                        // find destination
                    }
                    
                }
                else
                {
                    ai.target = targetPoint;
                }
            }
            //else
            //{
            //    if (safe)
            //    {
            //        ai.target = transform;
            //    }
            //    else
            //    {

            //    }
                
            //}
        }
        else
        {

        }
        
    }

    public void PlayerEnter()
    {
        chase = true;
    }

    public void PlayerLeave()
    {
        chase = false;
    }

    public void LightEnter()
    {
        safe = false;
    }

    public void LightLeave()
    {
        safe = true;
    }
}
