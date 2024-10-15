using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrawLight2DPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen position to world position
        // Set the Z value to 0 if you are working in 2D
        mousePosition.z = Camera.main.nearClipPlane;  // Adjust this value for the correct Z-depth in 3D
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the object's position to the mouse world position
        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
    }
}
