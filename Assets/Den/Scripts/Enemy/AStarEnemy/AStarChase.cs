using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AStarChase: MonoBehaviour
{
    private Transform target;
    public Transform idlePos;
    public string playerTag = "Player";

    private AIDestinationSetter ai;
    private PlayerController pc;

    [SerializeField]
    private AStarEnemyDetector ASEDetector;

    void Start()
    {
        ai = GetComponent<AIDestinationSetter>();
        pc = FindFirstObjectByType<PlayerController>();
        target = PlayerManager.Instance.PlayerTransform();
    }
    
    void Update()
    {
        if (ai.target != null && ASEDetector.chase) // if can chase
        {
            //Debug.Log("chase")
            if (pc.state.isInLightSource) // player is in light source
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
