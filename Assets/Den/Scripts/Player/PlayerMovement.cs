using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move Speed")]
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private float jumpPower = 12f;
    [SerializeField]
    private float climbSpeed = 1.3f;

    [Header("Check Settings")]
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask ladderLayer;
    public float distance;


    private float horizontal;
    private float vertical;
    private bool isGrounded = false;
    private Rigidbody2D rb;
    private Vector3 size;

    //sorry for this note! I just tried to test the higher ladder and I find this have some problem unsolved, so I decided to note it first.
    //if a ladder is here, this should mean that you would assign a ladder to it whenever the player starts to climb, just for sure
    //public Ladder ladder;

    private PlayerController playerController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        size = GetComponent<Transform>().localScale;
    }

    void Update()
    {
        Jump();
        Climb();

        if (!playerController.state.climb)
        {
            if (rb.velocity.y > 0) playerController.state.movingUp = true;
            else if (rb.velocity.y < 0) playerController.state.movingDown = true;
            else
            {
                playerController.state.movingUp = false;
                playerController.state.movingDown = false;
            }
            //an anim for climb would be great
        }
        else
        {
            playerController.state.movingUp = false;
            playerController.state.movingDown = false;
        }
    }

    void FixedUpdate()//Physics related updates
    {
        // Movement
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        GroundMovement();

        //Climb
        //the detecting area seems to be too small, or maybe it's the problem about the input timings?
    }

    private void GroundMovement()
    {
        if (playerController.state.stop)
        {
            // TODO - slow the player down smoothly

            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (!playerController.state.stop)
        {
            horizontal = Input.GetAxisRaw("Horizontal");//-1->left, 1->right

            // Moving left or right
            //I should change this into transform.position to precisely control the player position
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

            // Flipping
            if (horizontal != 0)
            {
                Flip(horizontal);
                playerController.state.horMoving = true;
            }
            else
            {
                playerController.state.horMoving = false;
            }
        }
    }

    private void Jump()
    {
        if (!playerController.state.stop)
        {
            // Jumping
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (Input.GetButtonDown("Jump") && playerController.state.climb)
            {
                //you can jump while you are climbing
                //this is dangerous
                playerController.state.climb = false;
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

    private void Climb()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, ladderLayer);
        if (hitInfo.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerController.state.climb = true;
                transform.position = new Vector3(hitInfo.collider.transform.position.x, transform.position.y, 0);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))//this would take a long time to be detected too
            {
                playerController.state.climb = false;
            }
        }
        else
        {
            playerController.state.climb = false;
        }

        if (playerController.state.climb && hitInfo.collider != null)
        {
            vertical = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbSpeed);
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 2.5f;
        }
    }
}

