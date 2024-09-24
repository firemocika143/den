using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float jumpPower = 12f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    private float horizontal;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private PlayerStatus status;
    private Vector3 size;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        status = GetComponent<PlayerStatus>();
        size = GetComponent<Transform>().localScale;
    }

    void Update()
    {
        Jump();
    }

    void FixedUpdate()//Physics related updates
    {
        // Movement
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        GroundMovement();
    }

    private void GroundMovement()
    {
        if (status.stop)
        {
            // TODO - slow the player down smoothly

            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (!status.stop)
        {
            horizontal = Input.GetAxisRaw("Horizontal");//-1->left, 1->right

            // Moving left or right
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            // Flipping
            if (horizontal != 0)
            {
                Flip(horizontal);
            }
        }
    }

    private void Jump()
    {
        if (!status.stop)
        {
            // Jumping
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }

    private void Flip(float dir)
    {
        transform.localScale = new Vector3(dir * size.x, 1 * size.y , 1);
    }

    //void ChangeAnimationState(string state)
    //{
    //    if (currentState == state) return;

    //    animator.Play(state);//I guess this function record the state by the current sprite
    //    currentState = state;
    //}
}

