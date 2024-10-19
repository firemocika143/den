using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject currOneWayPlatform;

    //note: these 2 objects are not allowed to be set in the ladder prefab because they are not parts included by the prefab, we'll need to fix this later
    [SerializeField]
    private BoxCollider2D playerCollider;
    //private PlayerController playerController;

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
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);

        //PlayerController pc = FindFirstObjectByType<PlayerController>();
        //if (pc != null)
        //{
        //    playerController.state.climb = true;
        //}
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        playerCollider = col.gameObject.GetComponent<BoxCollider2D>();
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D col)
    //{
    //    if (col.gameObject.CompareTag("Player"))
    //    {
    //        playerCollider = null;
    //    }
    //}
}