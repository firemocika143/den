using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public float minDistanceToStartAttack = 1f;
    public float maxDistanceToStartAttack = 1f;
    public Vector3 previousPosition;
    public bool isNewClick = false;
    public Transform AttackPoint;

    [SerializeField]
    private LineRenderer lineRenderer;

    void Start()
    {
        AttackPoint.position = new Vector3(AttackPoint.position.x, AttackPoint.position.y, 0);
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Draw();
        }
    }

    public void Draw()
    {
        if (isNewClick)
        {
            previousPosition = AttackPoint.position;
            isNewClick = false;
        }
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPosition.z = 0;
        float distance = Vector3.Distance(AttackPoint.position, currentPosition);

        if (distance > minDistanceToStartAttack && distance < maxDistanceToStartAttack)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPosition);
            previousPosition = currentPosition;
        }
    }
}
