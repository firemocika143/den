using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[DefaultExecutionOrder(10002)]
public class OneWayPlatform : MonoBehaviour
{
    private BoxCollider2D playerCollider;

    private bool onCollision = false;

    private void Start()
    {
        playerCollider = PlayerManager.Instance.PlayerCollider();
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.S) && onCollision)
        {
            if (playerCollider != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            onCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            onCollision = false;
        }
    }

    public void CollideWithCollider()
    {
        this.gameObject.SetActive(true);
    }

    public void LeaveCollider()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = this.gameObject.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.8f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}