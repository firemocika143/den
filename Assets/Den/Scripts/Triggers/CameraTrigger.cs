using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public PolygonCollider2D newCameraBound;

    public void switchCameraBound()
    {
        CameraManager.Instance.SwitchBound(newCameraBound);
    }
}
