using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField]
    private CinemachineVirtualCamera fst_vcam;
    [SerializeField]
    private List<CinemachineVirtualCamera> vcams;

    private CinemachineVirtualCamera curr_vcam;
    private Coroutine zoomCoroutine;

    private void Awake()
    {
        SwitchVirtualCamera(fst_vcam);
    }

    private void Start()
    {
        Instance = this;

        GameObject player = FindFirstObjectByType<PlayerController>().gameObject;
        if (player != null)
        {
            foreach (var c in vcams)
            {
                c.Follow = player.transform;
            }
        }
    }

    public void Follow(Transform target)
    {
        if (curr_vcam == null) return;

        curr_vcam.Follow = target;
    }

    public void SwitchVirtualCamera(CinemachineVirtualCamera vcam)
    {
        foreach (var c in vcams)
        {
            c.enabled = c == vcam;
            if (c == vcam) curr_vcam = c;
        }
    }

    public void Zoom(float targetRadius, float duration, float smoothness)
    {
        if (zoomCoroutine != null)
        {
            Debug.LogError("worng in camera");
            StopCoroutine(zoomCoroutine);
        }
            

        zoomCoroutine = StartCoroutine(lerpFieldOfView(targetRadius, duration));
    }

    IEnumerator lerpFieldOfView(float targetRadius, float duration)
    {
        float counter = 0;

        float fromRadius = curr_vcam.m_Lens.OrthographicSize;

        while (counter < duration)
        {
            counter += Time.deltaTime;

            float fOVTime = counter / duration;
            //Debug.Log(fOVTime);

            //Change FOV
            curr_vcam.m_Lens.OrthographicSize = Mathf.Lerp(fromRadius, targetRadius, fOVTime);
            //Wait for a frame
            yield return null;
        }
    }

    //private IEnumerator SmoothZoom(float newRadius, float time, float times)
    //{
    //    if (time <= 0 || curr_vcam.m_Lens.OrthographicSize == newRadius) yield break;

    //    float addAmount = newRadius - curr_vcam.m_Lens.OrthographicSize;

    //    for (int i = 0; i < times; i++)
    //    {
    //        //this can have smoothness and time as well
    //        yield return new WaitForSeconds(time / times);
    //        curr_vcam.m_Lens.OrthographicSize += addAmount / times;
    //    }

    //    Debug.Log("Done");
    //    curr_vcam.m_Lens.OrthographicSize = newRadius;
    //    zoomCoroutine = null;

    //    //lerpTimer = Time.deltaTime * smooth;
    //    //Camera.main.fieldOfView = Mathf.Lerp(initialFOV, zoomInFOV, lerpTimer);
    //}


}
