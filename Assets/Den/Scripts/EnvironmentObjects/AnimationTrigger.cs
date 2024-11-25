using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private AnimationHandler animHandler;
    [SerializeField]
    private string enterAnimation;
    [SerializeField]
    private string exitAnimation;
    [SerializeField]
    private bool dontShowIfPlayerIsInDanger = true;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (!PlayerManager.Instance.PlayerIsInDanger() || !dontShowIfPlayerIsInDanger)
            {
                animHandler.ChangeAnimationState(enterAnimation);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            animHandler.ChangeAnimationState(exitAnimation);
        }
    }
}
