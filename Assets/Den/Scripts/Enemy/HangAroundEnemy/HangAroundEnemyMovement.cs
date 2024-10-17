using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangAroundEnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;
    
    public Rigidbody2D rb;
    public GameObject lightSourceDetector;
    private bool reachLight;
    private bool escaping;
    private float escapeTime = 2.0f;
    private float horizontal;
    
    private Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(gameObject.name);
        size = GetComponent<Transform>().localScale;
        reachLight = lightSourceDetector.GetComponent<HangAroundEnemyLightSourceDetector>().reachLight;
        escaping = false;

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
        if (!escaping)
        {
            GroundMovement();
        }
    }

    private void GroundMovement()
    {
        if (rb.velocity.x < 0)
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 1;
        }
        reachLight = lightSourceDetector.GetComponent<HangAroundEnemyLightSourceDetector>().reachLight;
        if (reachLight)
        {
            horizontal *= -1;

            StartCoroutine(escapeCount());

        }
        else
        {
            float num = Random.Range(0.0f, 1.0f);
            if (num > 0.99f)
            {
                horizontal *= -1;
            }
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

    private IEnumerator escapeCount()
    {
        escaping = true;

        yield return new WaitForSeconds(escapeTime);

        escaping = false;
    }
}