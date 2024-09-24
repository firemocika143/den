using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private string filterTag = "Player";

    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(filterTag)) return;// it probably be annoying if any other object get into the trigger
        onTriggerEnter.Invoke();
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(filterTag)) return;
        onTriggerExit.Invoke();
    }

    public void DestroyTrigger()
    {
        Destroy(gameObject);
    }
}
