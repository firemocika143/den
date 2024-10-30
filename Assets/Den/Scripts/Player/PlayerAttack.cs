using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange;
    public LayerMask enemyLayer;
    public LayerMask triggerLayer;
    public float maxDistanceToStart = 10f;
    public int attack;

    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject drawLight;// why isn't this working?

    private Vector3 pos;
    private PlayerController playerController;
    private PlayerSkill skill;

    private void Start()
    {
        drawLight.SetActive(false);
        playerController = GetComponent<PlayerController>();
        skill = GetComponent<PlayerSkill>();
    }

    private void Update()
    {
        if (GameManager.Instance.gamePaused) return;

        if (!(skill.lightDraw == null))
        {
            if (playerController.state.lightEnergy <= 0 || playerController.state.stop)
            {
                if (skill.lightDraw.isDrawing) EndDraw();
            }
            else if (!playerController.state.stop)
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

        Collider2D[] hitTriggers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, triggerLayer);//what about using Raycast here?

        foreach (Collider2D trigger in hitTriggers)
        {
            Debug.Log("hitting a light trigger");
            //TODO - give damage to target enemy

            if (trigger.TryGetComponent<LightTrigger>(out var lt))
            {
                lt.Triggered();
            }
        }
    }

    //LightDraw
    public void ObtainLightDraw()
    {
        if (skill.lightDraw != null) return;

        skill.LightDrawInit(); 
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
            skill.lightDraw.LightDrawStart(attackPoint.position);
            playerController.state.attacking = true;
            playerController.state.attackEnd = false;
            //TODO - cost light energy with time
        }
    }

    private void UpdateDraw()
    {
        //TODO - if they are, while they drag the mouse, cost light energy and move attacking point position
        if (Input.GetMouseButton(0))
        {
            skill.lightDraw.LightDrawUpdate();

            pos = Input.mousePosition;
            pos.z = 0;
            attackPoint.position = Camera.main.ScreenToWorldPoint(pos);

            if (!drawLight.activeSelf) drawLight.SetActive(true);
            drawLight.transform.position = new Vector3(attackPoint.position.x, attackPoint.position.y, 0);

            Attack();

            if (Time.time - skill.lightDraw.costTimer > skill.lightDraw.costTime)
            {
                playerController.UseLightEnergy(skill.lightDraw.costPerTime);
                skill.lightDraw.costTimer = Time.time;
            }
        }
    }

    private void DetectEndDraw()
    {
        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }
    }

    private void EndDraw()
    {
        //playerController.state.attacking = false;//uh... now this should be in playeranimation for deciding the animation for player controller, that's bad
        playerController.state.attackEnd = true;
        attackPoint.position = transform.position;
        drawLight.SetActive(false);
        skill.lightDraw.LightDrawEnd();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        //visualize attack point range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
