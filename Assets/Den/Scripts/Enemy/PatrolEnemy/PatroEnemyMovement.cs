using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatroEnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private GameObject leftTarget;
    [SerializeField]
    private GameObject rightTarget;

    private Rigidbody2D rb;
    private float horizontal;
    private Vector3 size;
    private Transform currentTarget;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        size = GetComponent<Transform>().localScale;

        float num = Random.Range(0.0f, 1.0f);
        if (num > 0.5f)
        {
            FaceLeft();
        }
        else
        {
            FaceRight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        //if((Mathf.Abs(currentTarget.position.x -transform.position.x) < 0.5f) && (currentTarget == leftTarget.transform))
        if (currentTarget.position.x - transform.position.x > 0.5f)
        {
            FaceRight();
        } //else if ((Mathf.Abs(currentTarget.position.x - transform.position.x) < 0.5f) && (currentTarget == rightTarget.transform))
        else if (currentTarget.position.x - transform.position.x < 0.5f)
        {
            FaceLeft();
        } 
        else if (currentTarget == leftTarget.transform)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        } 
        else if (currentTarget == rightTarget.transform)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
    }

    private void FaceLeft()
    {
        currentTarget = leftTarget.transform;
        rb.velocity = new Vector2(-speed, rb.velocity.y);
        transform.localScale = new Vector3(-size.x, size.y, 1);
    }

    private void FaceRight()
    {
        currentTarget = rightTarget.transform;
        rb.velocity = new Vector2(speed, rb.velocity.y);
        transform.localScale = new Vector3(size.x, size.y, 1);
    }
}
