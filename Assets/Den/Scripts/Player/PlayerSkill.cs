using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float minDistanceToStart = 0.1f;
    public float maxDistanceToStart = 10f;
    public Vector3 previousPosition;
    public bool isNewClick = false;
    public Transform AttackPoint;

    private LineRenderer lineRenderer;

    void Start()
    {
        previousPosition = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0;
            float distance = Vector3.Distance(previousPosition, currentPosition);

            if (distance > minDistanceToStart && distance < maxDistanceToStart)
            {
                if (previousPosition == transform.position)//this is really a bad way
                {
                    lineRenderer.SetPosition(0, currentPosition);
                }
                else
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
                    previousPosition = currentPosition;
                }
            }
        }
    }
}
