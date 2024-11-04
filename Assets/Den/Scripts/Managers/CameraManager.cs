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
    private Coroutine zoom;

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

    public void Zoom(float newRadius, float zoomTime, float smoothness)
    {
        if (zoom != null)
        {
            // how to deal with this?
        }

        zoom = StartCoroutine(SmoothZoom(newRadius, zoomTime, smoothness));
    }

    private IEnumerator SmoothZoom(float newRadius, float time, float times)
    {
        if (time <= 0 || curr_vcam.m_Lens.OrthographicSize == newRadius) yield break;

        float addAmount = newRadius - curr_vcam.m_Lens.OrthographicSize;

        while (curr_vcam.m_Lens.OrthographicSize <= newRadius)
        {
            //this can have smoothness and time as well
            yield return new WaitForSeconds(time/times);
            curr_vcam.m_Lens.OrthographicSize += addAmount / times;
        }

        curr_vcam.m_Lens.OrthographicSize = newRadius;
        zoom = null;
    }
}
