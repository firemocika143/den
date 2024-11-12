using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charging : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private bool trigger = false;
    private bool isActivate = false;
    public bool castRequest, castSuccess, castInProgress;
    public float castTime;
    private float castStartTime;
    private SpriteRenderer deviceSprite;
    private PlayerController playerController;
    void Start()
    {
        deviceSprite = GetComponent<SpriteRenderer>();
        playerController = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        animator.enabled = false;

        castTime = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
       if (trigger && Input.GetKeyDown(GameManager.Instance.keySettings.LightLantern) && playerController.state.lightEnergy > 0 && !isActivate)
        {
            if (!castInProgress)
            {
                StartCoroutine(Cast());
            }
        }

        if (castRequest)
        {
            InProgress();

            if (Input.GetKeyUp(GameManager.Instance.keySettings.LightLantern) || playerController.state.isDamaged)
            {   
                CastFail();
            } 
        }
    }

    private IEnumerator Cast()
    {
        castInProgress = true;

        RequestCast();
        yield return new WaitUntil (() => castRequest == false);

        if(castSuccess)
        {
            Debug.Log("Cast was successful");
            isActivate = true;
        } else
        {
            animator.Play("Charging", 0, 0.0f);
            animator.speed = 0;
            Debug.Log("Cast was fail");
        }

        castInProgress = false;
    }

    private void RequestCast()
    {
        castRequest = true;
        castSuccess = false;
        castStartTime = Time.time;
        animator.enabled = true;
        animator.speed = 1;
        animator.Play("Charging", 0, 0.0f);
        Invoke(nameof(CastSuccess), castTime);
    }

    private void CastSuccess()
    {
        Debug.Log("Invoke");
        castRequest = false;
        castSuccess = true;
    }

    public void CastFail()
    {
        castRequest = false;
        castSuccess = false;
        CancelInvoke(nameof(CastSuccess));
    }

    private void InProgress()
    {
        float timePassed = Time.time - castStartTime;
        Debug.Log(timePassed);
    }

    public void OnTrigger()
    {
        trigger = true;
        
    }

    public void OffTrigger()
    {
        trigger = false;
    }
}
