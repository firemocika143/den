using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDarkKnightMovement : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2.0f;

    [Header("Knockback")]
    public float knockbackForce = 5.0f;
    public float knockbackDuration = 0.5f;

    private Transform target;
    private Rigidbody2D rb;
    private bool isKnockedBack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerManager.Instance.PlayerTransform();
    }

    void Update()
    {
        if (!isKnockedBack)
        {
            if (target.position.x - transform.position.x > 1.0f)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                Flip(-1);
            }
            else if (target.position.x - transform.position.x < -1.0f)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                Flip(1);
            }
        }
    }

    private void Flip(float dir)
    {
        transform.localScale = new Vector3(dir * Mathf.Abs(transform.localScale.x), 1 * transform.localScale.y, 1);
    }

    public void Knockback(Vector2 direction)
    {
        if (!isKnockedBack)
        {
            isKnockedBack = true;
            rb.velocity = Vector2.zero;
            rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(ResetKnockback());
        }
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }
}
