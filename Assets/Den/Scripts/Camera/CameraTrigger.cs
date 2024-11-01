using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;

    public void switchCameraBound()
    {
        CameraManager.Instance.SwitchVirtualCamera(vcam);
    }
}
