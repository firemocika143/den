using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWayPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject currOneWayPlatform;

    //note: these 2 objects are not allowed to be set in the ladder prefab because they are not parts included by the prefab, we'll need to fix this later
    [SerializeField]
    private BoxCollider2D playerCollider;
    [SerializeField]
    private PlayerController playerController;

    void Update() {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision());//there poped out an error message when I am climbing but not colliding on the top platform of the ladder and press S
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
        if (pc != null)
        {
            playerController.state.climb = true;
        }
    }
}