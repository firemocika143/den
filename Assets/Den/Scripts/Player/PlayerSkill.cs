using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Serializable]
    public class LightDraw
    {
        public float minDistanceToStart = 0.1f;
        public Vector3 previousPosition;

        public LineRenderer lineRenderer;
        public bool isDrawing = false;

        public float costTime = 0.2f;
        public int costPerTime = 1;
        public float costTimer;
    }

    [HideInInspector]
    public LightDraw lightDraw = null;

    private PlayerController playerController;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
        LightDrawInit();
    }

    private void Update()
    {
        if (!(lightDraw == null))
        {
            if (!lightDraw.isDrawing)
            {
                lightDraw.lineRenderer.positionCount = 1;
                lightDraw.lineRenderer.SetPosition(0, transform.position);
            }
        }
    }

    // LightDraw(showing track of light here) -> if I don;t put these in to the class, we wouldn't need to check if the player still haven't get lightDraw in PlayerAttack
    // or, maybe we should detect there to avoid useless calculations
    public void LightDrawInit()
    {
        if (lightDraw != null) return;

        lightDraw = new LightDraw();
        lightDraw.lineRenderer = GetComponent<LineRenderer>();
    }
    public void LightDrawStart(Vector3 startPos)
    {
        if (lightDraw == null) return;

        lightDraw.lineRenderer.positionCount = 1;
        lightDraw.lineRenderer.SetPosition(0, startPos);
        lightDraw.previousPosition = startPos;
        lightDraw.isDrawing = true;
        lightDraw.costTimer = Time.time;
    }

    public void LightDrawUpdate()
    {
        if (lightDraw == null) return;

        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPosition.z = 0;
        float distance = Vector3.Distance(lightDraw.previousPosition, currentPosition);

        if (distance > lightDraw.minDistanceToStart)//why do I need this?
        {
            lightDraw.lineRenderer.positionCount++;
            lightDraw.lineRenderer.SetPosition(lightDraw.lineRenderer.positionCount - 1, currentPosition);
            lightDraw.previousPosition = currentPosition;
        }

        if (Time.time - lightDraw.costTimer > lightDraw.costTime)
        {
            playerController.UseLightEnergy(lightDraw.costPerTime);
            lightDraw.costTimer = Time.time;
        }
    }

    public void LightDrawEnd()
    {
        if (lightDraw == null) return;

        lightDraw.lineRenderer.positionCount = 1;
        lightDraw.isDrawing = false;
    }
}
