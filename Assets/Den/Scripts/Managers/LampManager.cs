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

    private void Start()
    {
        Instance = this;

        lightArea.SetActive(true);
    }

    public IEnumerator TurnOffLampEvent()
    {
        yield return null;
        lightArea.SetActive(false);

        foreach (var lamp in lamps)
        {
            // It's quite lag here
            if (lamp.gameObject.activeSelf)
            {
                lamp.Off();
                yield return new WaitForSeconds(turnOffTime);
                // TODO - play SFX
                lamp.gameObject.SetActive(false);
                // TODO - place a death trigger at the lamp position(but then? how to control player respawn points? and return to which point of the game?, is it ok to just restart this function again?)
                Instantiate(killTriggerPrefab, lamp.transform.position, Quaternion.identity);
            }
        }
    }
}
