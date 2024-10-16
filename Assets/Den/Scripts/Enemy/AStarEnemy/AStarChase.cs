using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarChase: MonoBehaviour
{
    public Transform target;
    public Transform idlePos;
    public PlayerController pc;
    public string playerTag = "Player";

    private AIDestinationSetter ai;

    [SerializeField]
    private AStarEnemyDetector ASEDetector;

    void Start()
    {
        ai = GetComponent<AIDestinationSetter>();
    }
    
    void Update()
    {
        if (ai.target != null && ASEDetector.chase) // if can chase
        {
            //Debug.Log("chase")
            if (pc.isInLightSource) // player is in light source
            {
                SafeOrNot();
            }
            else
            {
                ai.target = target;
            }
        }
        else
        {
            //Debug.Log("don't chase");
            SafeOrNot();
        }
        
    }

    private void SafeOrNot()
    {
        if (ASEDetector.safe) // if is safe
        {
            ai.target = transform;
            //Debug.Log("safe");
        }
        else
        {
            // find destination
            ai.target = idlePos;
            //Debug.Log("unsafe");
        }
    }
}
