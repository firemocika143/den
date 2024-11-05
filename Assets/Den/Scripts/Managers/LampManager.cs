using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampManager : MonoBehaviour
{
    public static LampManager Instance { get; private set; }

    [SerializeField]
    private List<Lamp> lamps;
    [SerializeField]
    private GameObject killTriggerPrefab;

    public float turnOffTime;

    private int reopen;
    private IEnumerator turnOffEvent = null;// control the event here
    private List<GameObject> killTriggers = new List<GameObject>();
    //private int currLamp;
    //private int cid = 0;

    private void Start()
    {
        Instance = this;
    }

    public IEnumerator TurnOffLampEventCoroutine()
    {
        yield return null;
        //cid++;
        //Debug.Log(cid);

        foreach (Lamp lamp in lamps)
        {
            if (lamp.gameObject.activeSelf)
            {
                lamp.Off();
                // TODO - play SFX
                yield return new WaitForSeconds(turnOffTime);
            }
        }

        //while (currLamp < lamps.Count)
        //{
        //    int now = currLamp;
        //    // It's quite lag here
        //    if (lamps[currLamp].gameObject.activeSelf && currLamp >= reopen)
        //    {
        //        lamps[currLamp].Off();
        //        // TODO - play SFX
        //        yield return new WaitForSeconds(turnOffTime);
        //    }

        //    if (currLamp == now)
        //    {
        //        lamps[currLamp].gameObject.SetActive(false);
        //        // TODO - place a death trigger at the lamp position(but then? how to control player respawn points? and return to which point of the game?, is it ok to just restart this function again?)
        //        GameObject killTrigger = Instantiate(killTriggerPrefab, lamps[currLamp].transform.position, Quaternion.identity);
        //        killTriggers.Add(killTrigger);
        //        currLamp++;
        //    }
        //}
    }

    public void TurnOffLampEvent()
    {
        if (turnOffEvent == null)
        {
            turnOffEvent = TurnOffLampEventCoroutine();
        }

        StartCoroutine(turnOffEvent);
    }

    public void PauseTurnOffEvent()
    {
        if (turnOffEvent == null) return;

        StopCoroutine(turnOffEvent);
    }

    public void UpdateReopen(int updateLamp)
    {
        reopen = updateLamp;
    }

    //public void ReturnLamp()
    //{
    //    Debug.Log(reopen);
    //    Debug.Log(lamps.Count);
    //    if (reopen >= lamps.Count) return;

    //    foreach (var k in killTriggers)
    //    {
    //        Destroy(k);
    //    }

    //    killTriggers.Clear();

    //    //foreach (var lamp in lamps)
    //    //{
    //    //    lamp.gameObject.SetActive(true);
    //    //}

    //    for (int i = reopen; i < lamps.Count; i++)
    //    {
    //        var lamp = lamps[i];
    //        lamp.gameObject.SetActive(true);
    //        lamp.LightUp();
    //    }

    //    currLamp = 0;
    //    //it's working really weird here
    //}
}
