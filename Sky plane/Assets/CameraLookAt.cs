using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public PlaneControllerV2 playerPlane;
    public Vector3 cameraOffset;

    private void FixedUpdate()
    {
        transform.position = playerPlane.transform.position + cameraOffset;
    }
}
