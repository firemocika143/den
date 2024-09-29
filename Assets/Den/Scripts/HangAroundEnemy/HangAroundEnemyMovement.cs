using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangAroundEnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    
    public Rigidbody2D rb;

    private float horizontal;
    
    private Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(gameObject.name);
        size = GetComponent<Transform>().localScale;

        float num = Random.Range(0.0f, 1.0f);
        if (num > 0.5f)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        } else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        if(rb.velocity.x < 0)
        {
            horizontal = -1;
        }
        float num = Random.Range(0.0f, 1.0f);
        if (num > 0.99f )
        {
            horizontal *= -1;
        }
        // Moving left or right
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // Flipping
        if (horizontal != 0)
        {
            Flip(horizontal);
        }
    }

    private void Flip(float dir)
    {
        transform.localScale = new Vector3(dir * size.x, 1 * size.y, 1);
    }
}
