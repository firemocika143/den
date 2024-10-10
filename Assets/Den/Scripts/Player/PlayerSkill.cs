using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float minDistanceToStart = 0.1f;
    public Vector3 previousPosition;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // LightDraw(showing track of light here)
    public void LightDrawStart(Vector2 startPos)
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPos);
        previousPosition = startPos;
    }

    public void LightDrawUpdate()
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPosition.z = 0;
        float distance = Vector3.Distance(previousPosition, currentPosition);

        if (distance > minDistanceToStart)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
            previousPosition = currentPosition;
        }
    }
}
