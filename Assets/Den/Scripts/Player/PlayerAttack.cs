using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayer;

    private void Update()
    {
        //TODO - detect if the player is clicking in the attack trigger zone
        //TODO - if they are, while they drag the mouse, cost light energy and move attacking point position
        //TODO - once they stop dragging(GetMouseButtonUp) reset the attack point's position
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        //visualize attack point range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
