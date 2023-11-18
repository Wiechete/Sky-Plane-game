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
        if(transform.position.y <= 0.375f)
            transform.position =new Vector3(transform.position.x, 0.375f, transform.position.z);
    }
}
