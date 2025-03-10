using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationHandler animHandler;
    [SerializeField]
    private PlayerController playerController;

    //---------------------animation states-----------------------//
    [Serializable]
    public enum PlayerAnimationState
    {
        // movement(last)
        IDLE,
        WALK,
        JUMP,
        FALL,
        ATTACK,
        ATTACKEND,
        DIE,
        DOWN,
        REST,
        STAND
    }

    // movement(last)
    [Header("movement")]
    public string IDLE;
    public string WALK;
    public string JUMP;
    public string FALL;

    //attack(second)
    [Header("attack")]
    public string ATTACK;
    public float startTime;
    public string ATTACKEND;
    public float endTime;

    //state(highest)
    [Header("state")]
    public string DIE;
    public float dieTime;
    public string DOWN;
    public float downTime;
    public string STAND;
    public float standTime;
    public string REST;
    //---------------------animation states-----------------------//

    void Start()
    {
        if (!playerController.state.dying) ChangeTo();
    }

    void Update()
    {
        if (!playerController.state.dying) ChangeTo();
    }

    public void PlayerRespawnAnimation()
    {
        animHandler.ChangeAnimationState(IDLE);
    }

    public IEnumerator PlayerDieAnimation(Action afterAction)
    {
        animHandler.ChangeAnimationState(DIE);
        yield return new WaitForSeconds(dieTime);
        afterAction?.Invoke();
    }

    public IEnumerator PlayerDownAnimation(Action afterAction)
    {
        animHandler.ChangeAnimationState(DOWN);
        yield return new WaitForSeconds(downTime);
        afterAction?.Invoke();
    }

    public IEnumerator PlayerAttackEndAnimation()
    {
        if (animHandler.CheckCurrentState() == ATTACKEND) yield return null;
        else
        {
            animHandler.ChangeAnimationState(ATTACKEND);
            yield return new WaitForSeconds(endTime);
            playerController.state.attackEnd = false;//I admit that this is quite stupid
            if (animHandler.CheckCurrentState() == ATTACKEND) playerController.state.attacking = false;
        }
    }

    public IEnumerator PlayerStandAnimation()
    {
        if (animHandler.CheckCurrentState() == STAND) yield return null;
        else
        {
            playerController.state.stop = true;
            animHandler.ChangeAnimationState(STAND);
            yield return new WaitForSeconds(standTime);
            playerController.state.stop = false;//I admit that this is quite stupid
            playerController.state.standing = false;
            playerController.state.resting = false;
        }
    }

    private void ChangeTo()
    {
        switch (playerController.currState)
        {
            case PlayerAnimationState.IDLE:
                animHandler.ChangeAnimationState(IDLE);
                break;
            case PlayerAnimationState.WALK:
                animHandler.ChangeAnimationState(WALK);
                break;
            case PlayerAnimationState.JUMP:
                animHandler.ChangeAnimationState(JUMP);
                break;
            case PlayerAnimationState.FALL:
                animHandler.ChangeAnimationState(FALL);
                break;
            case PlayerAnimationState.ATTACK:
                animHandler.ChangeAnimationState(ATTACK);
                break;
            case PlayerAnimationState.ATTACKEND:
                StartCoroutine(PlayerAttackEndAnimation());
                break;
            case PlayerAnimationState.DIE:
                animHandler.ChangeAnimationState(DIE);
                break;
            case PlayerAnimationState.DOWN:
                animHandler.ChangeAnimationState(DOWN);
                break;
            case PlayerAnimationState.REST:
                animHandler.ChangeAnimationState(REST);
                break;
            case PlayerAnimationState.STAND:
                StartCoroutine(PlayerStandAnimation());
                break;
        }
    }
}
