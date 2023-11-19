using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed;
    public PlaneControllerV2 planeController;
    public Renderer backgroundRenderer;
    // Update is called once per frame
    void FixedUpdate()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(planeController.currentSpeed * backgroundSpeed * Time.fixedDeltaTime, 0f);
        transform.position += new Vector3(planeController.currentSpeed * Time.fixedDeltaTime, 0, 0);
    }
}
