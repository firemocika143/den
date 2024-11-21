using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerSkill;

public class LightDraw
{
    public float minDistanceToStart = 0.1f;
    public Vector3 previousPosition;

    public LineRenderer lineRenderer;
    public bool isDrawing = false;

    public float costTime = 0.05f;
    public int costPerTime = 1;
    public float costTimer;

    // LightDraw(showing track of light here) -> if I don't put these in to the class, we wouldn't need to check if the player still haven't get lightDraw in PlayerAttack
    // or, maybe we should detect there to avoid useless calculations
    public void LightDrawStart(Vector3 startPos)
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPos);
        previousPosition = startPos;
        isDrawing = true;
        costTimer = Time.time;
    }

    public void LightDrawUpdate()
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPosition.z = 0;
        float distance = Vector3.Distance(previousPosition, currentPosition);

        if (distance > minDistanceToStart)//why do I need this?
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
            previousPosition = currentPosition;
        }
    }

    public void LightDrawEnd()
    {
        lineRenderer.positionCount = 1;
        isDrawing = false;
    }
}
