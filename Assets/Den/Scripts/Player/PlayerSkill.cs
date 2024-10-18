using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
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
}
