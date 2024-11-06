using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkKnightMovement : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 6.0f;

    private Transform target = PlayerManager.Instance.PlayerTransform();

    private Rigidbody2D rb;
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = FindFirstObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        target = PlayerManager.Instance.PlayerTransform();
        if (target.position.x - rb.transform.position.x > 1.0f)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (target.position.x - rb.transform.position.x < 1.0f) 
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        Debug.Log(target.position.x);
    }
}
