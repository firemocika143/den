using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HigherLadder : MonoBehaviour
{
    [System.Serializable]
    public class LadderRange
    {
        public float low;
        public float high;

        public LadderRange(float l, float h)
        {
            low = l;
            high = h;
        }
    }

    public LadderRange range;
    public float climbSpeed;

    private float playerVerticlePosition;
    private bool playerClimbing;

    private void Start()
    {
        range = new LadderRange(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
    }

    public float NextPosition(float dir)
    {
        return climbSpeed * dir;
    }

    public void Climbed(float playerY)
    {
        playerVerticlePosition = playerY;
        playerClimbing = true;
    }
}
