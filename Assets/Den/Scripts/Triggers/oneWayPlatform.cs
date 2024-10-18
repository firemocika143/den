using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject currOneWayPlatform;
    [SerializeField]
    private BoxCollider2D playerCollider;
    [SerializeField]
    private PlayerController playerController;

    void Update() {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currOneWayPlatform != null)
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
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);

        PlayerController pc = FindFirstObjectByType<PlayerController>();
        if (pc!= null)
        {
            playerController.state.climb = true;
        }
    }
}