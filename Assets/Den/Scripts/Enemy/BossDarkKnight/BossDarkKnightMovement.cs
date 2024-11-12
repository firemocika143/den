using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkKnightMovement : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2.0f;

    private Transform target;

    private Rigidbody2D rb;
    private PlayerController pc;


    public BossDarkKnight bossDarkKnight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = FindFirstObjectByType<PlayerController>();
        target = PlayerManager.Instance.PlayerTransform();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossDarkKnight.playerIsInBossDarkKnightArea)
        {
            if (target.position.x - rb.transform.position.x > 1.0f)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                Flip(1);
            }
            else if (target.position.x - rb.transform.position.x < -1.0f)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                Flip(-1);
            }
            //Debug.Log(target.position.x);
        }
    }

    private void Flip(float dir)
    {
        transform.localScale = new Vector3(dir * transform.localScale.x, 1 * transform.localScale.y, 1);
    }
}
