using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarChase: MonoBehaviour
{
    public Transform targetPoint;
    public Transform idlePoint;
    public PlayerController pc;
    private AIDestinationSetter ai;

    public GameObject playerDetector;
    public GameObject lightSourceDetector;
    public bool chase;
    public bool safe;
    private string playerTag;
    private string lightSourceTag;

    void Start()
    {
        ai = GetComponent<AIDestinationSetter>();
        chase = playerDetector.GetComponent<AStarEnemyPlayerDetector>().chase;
        safe = lightSourceDetector.GetComponent<AStarEnemyLightSourceDetector>().safe;
        playerTag = "Player";
        lightSourceTag = "LightSorce";
    }
    
    void Update()
    {
        chase = playerDetector.GetComponent<AStarEnemyPlayerDetector>().chase;
        safe = lightSourceDetector.GetComponent<AStarEnemyLightSourceDetector>().safe;
        if (ai.target != null && chase) // if can chase
        {
            //Debug.Log("chase")
            if (pc.isInLightSource) // player is in light source
            {
                SafeOrNot(safe);

            }
            else
            {
                ai.target = targetPoint;
            }
        }
        else
        {
            //Debug.Log("don't chase");
            SafeOrNot(safe);
        }
        
    }

    private void SafeOrNot(bool safe)
    {
        if (safe) // if is safe
        {
            ai.target = transform;
            //Debug.Log("safe");
        }
        else
        {
            // find destination
            ai.target = idlePoint;
            //Debug.Log("unsafe");
        }
    }
}
