using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField]
    private CinemachineVirtualCamera v_cm;

    private void Start()
    {
        Instance = this;
    }

    public void FixCamera(Transform target)
    {
        if (v_cm == null) return;

        v_cm.Follow = null;
        v_cm.Follow = target;
    }

    public void Follow(Transform target)
    {
        if (v_cm == null) return;

        v_cm.Follow = target;
    }
}
