using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public LayerMask enemyLayer;
    public float maxDistanceToStart = 10f;
    public int attack;

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

            if (enemy.TryGetComponent<IEnemy>(out var e))
            {
                e.Damage(attack);
            }
        }
    }

    private void OnMouseDown()
    {
        pos = Input.mousePosition;
        pos.z = 0;
        //TODO - detect if the player is clicking in the attack trigger zone
        if (Input.GetMouseButton(0) && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(pos)) < maxDistanceToStart)//  -> but I can also use the collider to restrict the range
        {
            attackPoint.position = Camera.main.ScreenToWorldPoint(pos);
            Attack();
            drawLight.enabled = true;
            skill.LightDrawStart(attackPoint.position);
            //TODO - cost light energy with time
        }
    }

    
    private void OnMouseDrag()
    {
        //TODO - if they are, while they drag the mouse, cost light energy and move attacking point position
        if (Input.GetMouseButton(0))
        {
            skill.LightDrawUpdate();

            pos = Input.mousePosition;
            pos.z = 0;
            attackPoint.position = Camera.main.ScreenToWorldPoint(pos);

            Attack();
        }
    }

    
    private void OnMouseUp()
    {
        //TODO - once they stop dragging(GetMouseButtonUp) reset the attack point's position
        //if (Input.GetMouseButton(0))
        //{
        attackPoint.position = transform.position;
        drawLight.enabled = false;
        skill.LightDrawEnd();
        //}
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        //visualize attack point range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
