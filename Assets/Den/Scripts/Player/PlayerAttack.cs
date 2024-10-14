using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public LayerMask enemyLayer;
    public float maxDistanceToStart = 10f;

    [SerializeField]
    private PlayerSkill skill;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private Light2D drawLight;// why isn't this working?

    private Vector3 pos;

    private void Start()
    {
        drawLight.enabled = false;
    }

    private void Update()
    {

    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);//what about using Raycast here?

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hitting an enemy");
            //TODO - give damage to target enemy
        }
    }

    //TODO - detect if the player is clicking in the attack trigger zone
    private void OnMouseDown()
    {
        pos = Input.mousePosition;
        pos.z = 0;

        if (Input.GetMouseButton(0) && Vector3.Distance(transform.position, Camera.main.ScreenToWorldPoint(pos)) < maxDistanceToStart)
        {
            attackPoint.position = Camera.main.ScreenToWorldPoint(pos);
            Attack();
            drawLight.enabled = true;
            skill.LightDrawStart(attackPoint.position);
            //TODO - cost light energy with time
        }
    }

    //TODO - if they are, while they drag the mouse, cost light energy and move attacking point position
    private void OnMouseDrag()
    {
        if (Input.GetMouseButton(0))
        {
            skill.LightDrawUpdate();

            pos = Input.mousePosition;
            pos.z = 0;
            attackPoint.position = Camera.main.ScreenToWorldPoint(pos);

            Attack();
        }
    }

    //TODO - once they stop dragging(GetMouseButtonUp) reset the attack point's position
    private void OnMouseUp()
    {
        attackPoint.position = transform.position;
        drawLight.enabled = false;
        skill.LightDrawEnd();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        //visualize attack point range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
