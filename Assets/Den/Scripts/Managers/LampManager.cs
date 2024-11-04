using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    public static LampManager Instance { get; private set; }

    [SerializeField]
    private List<Lamp> lamps;
    [SerializeField]
    private GameObject lightArea;
    [SerializeField]
    private GameObject killTriggerPrefab;

    public float turnOffTime;

    private int reopen;
    private Coroutine turnOffEvent = null;// control the event here
    private StreetFlow sf;
    private List<GameObject> killTriggers = new List<GameObject>();
    private int currLamp;

    private void Start()
    {
        Instance = this;

        sf = (StreetFlow)GameManager.Instance.flow;

        lightArea.SetActive(true);
    }

    public IEnumerator TurnOffLampEventCoroutine()
    {
        yield return null;
        lightArea.SetActive(false);

        while (currLamp < lamps.Count)
        {
            // It's quite lag here
            if (lamps[currLamp].gameObject.activeSelf)
            {
                lamps[currLamp].Off();
                yield return new WaitForSeconds(turnOffTime);
                // TODO - play SFX
                lamps[currLamp].gameObject.SetActive(false);
                // TODO - place a death trigger at the lamp position(but then? how to control player respawn points? and return to which point of the game?, is it ok to just restart this function again?)
                GameObject killTrigger = Instantiate(killTriggerPrefab, lamps[currLamp].transform.position, Quaternion.identity);
                killTriggers.Add(killTrigger);
            }
            else
            {
                GameObject killTrigger = Instantiate(killTriggerPrefab, lamps[currLamp].transform.position, Quaternion.identity);
                killTriggers.Add(killTrigger);
            }

            currLamp++;
        }
    }

    public void TurnOffLampEvent()
    {
        //if (turnOffEvent != null)
        //{
        //    StopCoroutine(turnOffEvent);
        //    turnOffEvent = null;// then leak, but how to fix this?
        //    // and this may even be restart again...
        //}

        turnOffEvent = StartCoroutine(TurnOffLampEventCoroutine());
    }

    public void UpdateReopen(int updateLamp)
    {
        reopen = updateLamp;
    }

    public void ReturnLamp()
    {
        if (reopen == lamps.Count - 1) return;

        for (int i = 0; i < killTriggers.Count; i++)
        {
            Destroy(killTriggers[i]);
            killTriggers[i] = null;
        }

        killTriggers.Clear();

        for (int i = reopen; i < lamps.Count; i++)
        {
            var lamp = lamps[i];
            lamp.gameObject.SetActive(true);
            Debug.Log("RelightUp");
            lamp.LightUp();
        }

        currLamp = 0;//
    }
}
