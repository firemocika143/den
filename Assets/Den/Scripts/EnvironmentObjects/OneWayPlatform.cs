using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[DefaultExecutionOrder(10002)]
public class OneWayPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject currOneWayPlatform;

    private BoxCollider2D playerCollider;

    private void Start()
    {
        playerCollider = PlayerManager.Instance.PlayerCollider();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currOneWayPlatform != null && playerCollider != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }

    public void CollideWithCollider()
    {
        currOneWayPlatform.SetActive(true);
    }

    public void LeaveCollider()
    {
        currOneWayPlatform.SetActive(false);
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currOneWayPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.8f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}