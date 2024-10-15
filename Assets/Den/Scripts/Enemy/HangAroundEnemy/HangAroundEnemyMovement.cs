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
    public GameObject lightSourceDetector;
    private bool reachLight;
    private int dir;

    private float horizontal;
    
    private Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(gameObject.name);
        size = GetComponent<Transform>().localScale;
        reachLight = lightSourceDetector.GetComponent<HangAroundEnemyLightSourceDetector>().reachLight;
        dir = lightSourceDetector.GetComponent<HangAroundEnemyLightSourceDetector>().dir;

        float num = Random.Range(0.0f, 1.0f);
        if (num > 0.5f)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            horizontal = -1;
        } else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            horizontal = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        reachLight = lightSourceDetector.GetComponent<HangAroundEnemyLightSourceDetector>().reachLight;
        GroundMovement();
    }

    private void GroundMovement()
    {
        if(reachLight)
        {
            if (rb.velocity.x < 0)
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 1;
            }
            horizontal *= dir;
            // Moving left or right
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            if (rb.velocity.x < 0)
            {
                horizontal = -1;
            }
            float num = Random.Range(0.0f, 1.0f);
            if (num > 0.99f)
            {
                horizontal *= -1;
            }
            // Moving left or right
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

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