using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetFlow : Flow, IDataPersistence
{
    // should I make an static instance for this script?
    [SerializeField]
    private GameObject firstLamp;
    [SerializeField]
    private Transform centerPointOfLamps;
    [SerializeField]
    private float zoomOutRadius;

    private bool first;
    //private bool inStreetLightOffEvent;

    public void Awake()
    {
        name = "Street";
    }

    public override void StartFlow()
    {
        if (first)
        {
            // TODO - Loading
            // TODO - RespawnPlayer
            // TODO - Fade in
            UIManager.Instance.FadeIn();
            // SFX & VFX
            SoundManager.Instance.PlayInLightSource();

            first = false;
        }
    }

    public IEnumerator StreetLightOff()
    {
        yield return new WaitForSeconds(0.5f);
        //inStreetLightOffEvent = true;// I should cosider about where to turn this into false   
        //TODO - move Camera to see the first light flashes then turns off, then zoom out, then the second as well then get back to player then play music in danger
        CameraManager.Instance.Follow(firstLamp.transform);
        firstLamp.GetComponent<Lamp>().Off();
        yield return new WaitForSeconds(LampManager.Instance.turnOffTime);
        firstLamp.SetActive(false);
        // TODO - play tensive SFX for 0.5 second
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(LampManager.Instance.TurnOffLampEvent());
        CameraManager.Instance.Follow(centerPointOfLamps);
        CameraManager.Instance.Zoom(zoomOutRadius, 0.5f, 40);
        yield return new WaitForSeconds(LampManager.Instance.turnOffTime);
        // TODO - play tensive SFX for 1 second
        CameraManager.Instance.Follow(PlayerManager.Instance.PlayerTransform());
        CameraManager.Instance.Zoom(5f, 1f, 40);
        PlayerManager.Instance.EnablePlayerToMove();
        SoundManager.Instance.PlayInDanger();
        // this wouldn't work successfully for now, beacause the player controller itself call the get Into light source sound on it own and player is in the light source though they will have to leave there later
    }

    // there should be a function here for that if the player achieve some conditions(like they died or something), then we should return the flow back to some point, and then use this function to continue the flow
    // but where to call this function? In the Update function in this script? to check the player's state by PlayerManager?

    public void LoadData(GameData data)
    {
        //first = !data.Street;
    }

    public void SaveData(ref GameData data)
    {
        //data.Street = !first;
    }
}
