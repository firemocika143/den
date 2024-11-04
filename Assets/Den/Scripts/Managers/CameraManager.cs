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

    public IEnumerator SmoothZoom(float newRadius)
    {
        yield return null;

        while (curr_vcam.m_Lens.OrthographicSize <= newRadius)
        {
            //this can have smoothness and time as well
            yield return new WaitForSeconds(0.1f);
            curr_vcam.m_Lens.OrthographicSize += 1f;
        }

        curr_vcam.m_Lens.OrthographicSize = newRadius;
    }
}
