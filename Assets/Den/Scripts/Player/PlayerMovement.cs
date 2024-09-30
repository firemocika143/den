using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        //sorry for this note! I just tried to test the higher ladder and I find this have some problem unsolved, so I decided to note it first.
        //Climb();
    }

    void FixedUpdate()//Physics related updates
    {
        // Movement
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        GroundMovement();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ladder"))
        {
            var tempLadder = col.gameObject.GetComponent<HigherLadder>();
            Climbs(tempLadder);
        }
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
        if (!playerController.state.stop)
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

    private void Climb()
    {
        //sorry for this note! I just tried to test the higher ladder and I find this have some problem unsolved, so I decided to note it first.

        //float hDirection = Input.GetAxis("Horizontal");
        //if (playerController.state.climb && Mathf.Abs(Input.GetAxis("Vertical")) > .1f)
        //{
        //    rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //    transform.position = new Vector3(ladder.transform.position.x, rb.position.y, 0);
        //}
    }

    public void ClimbLadder(float ladderX)
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 0;
        transform.position = new Vector3(ladderX, rb.position.y, 0);
    }

    public void Climbing(float speed)
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }

    public void Climbs(HigherLadder ladder)
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (!playerController.state.climb)
            {
                playerController.state.climb = true;
                ClimbLadder(ladder.transform.position.x);
                ladder.Climbed(transform.position.y);
            }
            else
            {
                if (transform.position.y < ladder.range.high)
                {
                    Climbing(ladder.NextPosition(1f));
                }
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (!playerController.state.climb)
            {
                playerController.state.climb = true;
                ClimbLadder(ladder.transform.position.x);
                ladder.Climbed(transform.position.y);
            }
            else
            {
                if (transform.position.y > ladder.range.low)
                {
                    Climbing(ladder.NextPosition(-1f));
                }
            }
        }

        //if (state.climb && (transform.position.y - ladder.range.low > 2f))
        //{
        //    state.stop = true;
        //}
    }
}

