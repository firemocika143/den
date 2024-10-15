using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float minDistanceToStart = 0.1f;
    public Vector3 previousPosition;

    private LineRenderer lineRenderer;
    private bool drawing = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (!drawing)
        {
            lineRenderer.positionCount = 1;
            lineRenderer.SetPosition(0, transform.position);
        }
    }

    // LightDraw(showing track of light here)
    public void LightDrawStart(Vector3 startPos)
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, startPos);
        previousPosition = startPos;
        drawing = true;
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

    public void LightDrawEnd()
    {
        lineRenderer.positionCount = 1;
        drawing = false;
    }
}
