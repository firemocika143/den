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
    private GameObject drawLight;// why isn't this working?

    private Vector3 pos;

    private void Start()
    {
        drawLight.SetActive(false);
    }

    private void Update()
    {
        if (!(skill.lightDraw == null))
        {
            if (skill.lightDraw.isDrawing)
            {
                UpdateDraw();
                DetectEndDraw();
            }
            else
            {
                DetectDrawStart();
            }
        }
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

    private void DetectDrawStart()
    {
        pos = Input.mousePosition;
        pos.z = 0;
        //TODO - detect if the player is clicking in the attack trigger zone
        if (Input.GetMouseButtonDown(0) && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(pos)) < maxDistanceToStart)//  -> but I can also use the collider to restrict the range
        {
            attackPoint.position = Camera.main.ScreenToWorldPoint(pos);
            Attack();
            drawLight.SetActive(true);
            skill.LightDrawStart(attackPoint.position);
            //TODO - cost light energy with time
        }
    }

    private void UpdateDraw()
    {
        //TODO - if they are, while they drag the mouse, cost light energy and move attacking point position
        if (Input.GetMouseButton(0))
        {
            skill.LightDrawUpdate();

            pos = Input.mousePosition;
            pos.z = 0;
            attackPoint.position = Camera.main.ScreenToWorldPoint(pos);
            drawLight.transform.position = new Vector3(attackPoint.position.x, attackPoint.position.y, 0);

            Attack();
        }
    }

    private void DetectEndDraw()
    {
        if (Input.GetMouseButtonUp(0))
        {
            attackPoint.position = transform.position;
            drawLight.SetActive(false);
            skill.LightDrawEnd();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        //visualize attack point range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
