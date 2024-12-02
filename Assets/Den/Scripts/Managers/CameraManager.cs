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

        if (PlayerManager.Instance == null) Debug.LogError("PlayerManager haven't set it's instance");

        foreach (var c in vcams)
        {
            c.Follow = PlayerManager.Instance.PlayerTransform();
        }

        curr_vcam = fst_vcam;
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
            //c.enabled = c == vcam;
            //if (c == vcam) curr_vcam = c;

            c.gameObject.SetActive(c == vcam);
            if (c == vcam) curr_vcam = c;
        }
    }

    public void SwitchOtherCamera(CinemachineVirtualCamera vcam)
    {
        if (vcam == null)
        {
            Debug.LogError("no virtual camera assigned");
            return;
        }

        vcam.enabled = true;

        foreach (var c in vcams)
        {
            c.enabled = false;
        }
    }

    public void SwitchBackToMainCamera()
    {
        SwitchVirtualCamera (fst_vcam);
    }

    public void Zoom(float targetRadius, float duration, float smoothness)
    {
        if (zoomCoroutine != null)
        {
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
}
