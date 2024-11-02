using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetFlow : MonoBehaviour, IFlow, IDataPersistence
{
    [SerializeField]
    private string flowName = "Street";

    private bool first;

    public void Awake()
    {
        name = flowName;
    }

    public void StartFlow()
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

    public void LoadData(GameData data)
    {
        //first = !data.Street;
    }

    public void SaveData(ref GameData data)
    {
        //data.Street = !first;
    }
}
